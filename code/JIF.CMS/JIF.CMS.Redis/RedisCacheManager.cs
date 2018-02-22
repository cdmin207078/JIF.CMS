using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JIF.CMS.Core.Configuration.JIFConfig;

namespace JIF.CMS.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private static object _lock = new object();

        private static RedisConnectionWrapper _connectionWarpper;
        private readonly IDatabase _db;

        public RedisCacheManager(RedisConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config.Server))
                throw new Exception("Redis connection string is empty");

            initWrapper(config);

            _db = _connectionWarpper.GetDatabase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void initWrapper(RedisConfiguration config)
        {
            // 首先是 ConnectionMultiplexer 的封装，ConnectionMultiplexer对象是StackExchange.Redis最中枢的对象。
            // 这个类的实例需要被整个应用程序域共享和重用的，所以不需要在每个操作中不停的创建该对象的实例，一般都是使用单例来创建和存放这个对象，这个在官网上也有说明。
            // http://www.cnblogs.com/qtqq/p/5951201.html
            // https://stackexchange.github.io/StackExchange.Redis/Basics
            //builder.RegisterType<RedisConnectionWrapper>().As<RedisConnectionWrapper>().SingleInstance();

            if (_connectionWarpper != null)
                return;

            lock (_lock)
            {
                if (_connectionWarpper != null)
                    return;

                _connectionWarpper = new RedisConnectionWrapper(config);
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            var type = typeof(T);

            #region Primitive Type. string, int, long, decimal, double, bool

            if (type.IsPrimitive
                || type == typeof(string)
                || type == typeof(decimal)
                || type == typeof(DateTime)
                )
            {
                var data = _db.StringGet(key);
                return (T)Convert.ChangeType(data.ToString(), typeof(T));
            }

            #endregion

            #region Enum

            if (type.IsEnum)
            {
                return (T)Enum.Parse(typeof(T), _db.StringGet(key));
            }

            #endregion

            #region IEnumerable<T>

            if (type.IsGenericType)
            {
                var data = _db.StringGet(key);
                return data.IsNull ? default(T) : JsonConvert.DeserializeObject<T>(data);
            }

            #endregion

            #region User Complex-Model

            if (type.IsClass && type != typeof(string))
            {
                var data = _db.StringGet(key);
                return data.IsNull ? default(T) : JsonConvert.DeserializeObject<T>(data);

                //var data = _db.HashGetAll(key).ToDictionary(k => k.Name, v => v.Value);
                //if (data == null || data.Count == 0)
                //    return default(T);

                //var obj = Activator.CreateInstance<T>();
                ////var obj = Activator.CreateInstance(typeof(T));

                //foreach (var p in type.GetProperties())
                //{
                //    if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                //    {
                //        p.SetValue(obj, Convert.ChangeType(JsonConvert.DeserializeObject(data[p.Name]), p.PropertyType, null));
                //    }
                //    else
                //    {
                //        p.SetValue(obj, Convert.ChangeType(data[p.Name], p.PropertyType));
                //    }
                //}

                //return obj;
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

        public void Set<T>(string key, T data, TimeSpan? cacheTime = null)
        {
            var type = typeof(T);

            #region Primitive Type. string, int, long, decimal, double, bool

            if (type.IsPrimitive
                || type == typeof(string)
                || type == typeof(decimal)
                || type == typeof(DateTime)
                )
            {
                //_db.StringSet(key, JsonConvert.SerializeObject(data), cacheTime);
                _db.StringSet(key, data.ToString(), cacheTime);
                return;
            }

            #endregion

            #region Enum

            if (type.IsEnum)
            {
                _db.StringSet(key, Convert.ToInt32(data), cacheTime); // 存入数值
                //_db.StringSet(key, data.ToString(), cacheTime); // 存入名称
                return;
            }

            #endregion

            #region IEnumerable<T>

            if (type.IsGenericType)
            {
                _db.StringSet(key, JsonConvert.SerializeObject(data), cacheTime);
                return;
            }

            #endregion

            #region User Complex-Model

            if (type.IsClass && type != typeof(string))
            {
                _db.StringSet(key, JsonConvert.SerializeObject(data), cacheTime);
                return;

                //var hes = new List<HashEntry>();

                ////PropertyInfo[] properties;

                ////if (typePropertyCache.ContainsKey(typeof(T)))
                ////{
                ////    properties = typePropertyCache[typeof(T)];
                ////}
                ////else
                ////{
                ////    properties = ;
                ////    typePropertyCache.Add(typeof(T), properties);
                ////}

                //foreach (var p in typeof(T).GetProperties())
                //{
                //    if (p.PropertyType.IsClass && p.PropertyType != typeof(string))
                //        hes.Add(new HashEntry(p.Name, JsonConvert.SerializeObject(p.GetValue(data))));
                //    else
                //        hes.Add(new HashEntry(p.Name, p.GetValue(data).ToString()));
                //}

                //_db.HashSet(key, hes.ToArray());

                //return;
            }

            #endregion

            throw new ArgumentException("Redis Set type Unknow");
        }
    }
}
