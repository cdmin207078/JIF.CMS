using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly RedisConnectionWrapper _connectionWarpper;

        private readonly IDatabase _db;

        public RedisCacheManager(RedisConnectionWrapper connectionWarpper, JIFConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.RedisConfig.Server))
                throw new Exception("Redis connection string is empty");

            _connectionWarpper = connectionWarpper;

            _db = _connectionWarpper.GetDatabase();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            var type = typeof(T);

            #region Primitive Type. string, int, long, decimal, double, bool, enum

            if (type.IsPrimitive || type == typeof(string))
            {
                return JsonConvert.DeserializeObject<T>(_db.StringGet(key));
            }

            #endregion

            #region Enum

            if (type.IsEnum)
            {
                return JsonConvert.DeserializeObject<T>(_db.StringGet(key));
            }

            #endregion

            #region IEnumerable<T>

            if (type.IsGenericType)
            {
                return default(T);
            }

            #endregion

            #region User Complex-Model

            if (type.IsClass && type != typeof(string))
            {
                var data = _db.HashGetAll(key).ToDictionary(k => k.Name, v => v.Value);
                if (data == null || data.Count == 0)
                    return default(T);

                var obj = Activator.CreateInstance<T>();
                //var obj = Activator.CreateInstance(typeof(T));

                foreach (var p in type.GetProperties())
                {
                    if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                    {
                        p.SetValue(obj, Convert.ChangeType(JsonConvert.DeserializeObject(data[p.Name]), p.PropertyType, null));
                    }
                    else
                    {
                        p.SetValue(obj, Convert.ChangeType(data[p.Name], p.PropertyType));
                    }
                }

                return obj;
            }

            #endregion

            throw new ArgumentException("Redis Get type Unknow");
        }

        public bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T data, int? cacheTimeStamp)
        {
            var type = typeof(T);

            #region Primitive Type. string, int, long, decimal, double, bool, enum

            if (type.IsPrimitive || type == typeof(string))
            {
                _db.StringSet(key, JsonConvert.SerializeObject(data));
                return;
            }

            #endregion

            #region Enum

            if (type.IsEnum)
            {
                _db.StringSet(key, data.ToString());
                return;
            }

            #endregion

            #region IEnumerable<T>

            if (type.IsGenericType)
            {
                return;
            }

            #endregion

            #region User Complex-Model

            if (type.IsClass && type != typeof(string))
            {
                var hes = new List<HashEntry>();

                //PropertyInfo[] properties;

                //if (typePropertyCache.ContainsKey(typeof(T)))
                //{
                //    properties = typePropertyCache[typeof(T)];
                //}
                //else
                //{
                //    properties = ;
                //    typePropertyCache.Add(typeof(T), properties);
                //}

                foreach (var p in typeof(T).GetProperties())
                {
                    if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                        hes.Add(new HashEntry(p.Name, JsonConvert.SerializeObject(p.GetValue(data))));
                    else
                        hes.Add(new HashEntry(p.Name, p.GetValue(data).ToString()));
                }

                _db.HashSet(key, hes.ToArray());

                return;
            }

            #endregion
        }
    }
}
