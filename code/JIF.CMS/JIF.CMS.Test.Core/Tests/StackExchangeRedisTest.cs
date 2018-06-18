using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System.Net;
using System.Collections.Generic;
using JIF.CMS.Test.Core.Entities;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using System.Reflection;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class StackExchangeRedisTest
    {
        //private readonly string _connectionString = "192.168.0.118,password=foobared";
        private readonly string _connectionString = "192.168.0.130:6379";

        private readonly object _lock = new object();

        private ConnectionMultiplexer _connection;

        private ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                if (_connection != null)
                {
                    _connection.Dispose();
                }

                _connection = ConnectionMultiplexer.Connect(_connectionString);
            }

            return _connection;
        }

        private IDatabase GetDatabase(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? -1);
        }

        private IServer GetServer(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

        private EndPoint[] GetEndPoints()
        {
            return GetConnection().GetEndPoints();
        }

        private void FlushDatabase(int? db = null)
        {
            var endPoints = GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                GetServer(endPoint).FlushDatabase(db ?? -1);
            }
        }

        [TestMethod]
        public void Server_Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Increment_Test()
        {
            var db = GetDatabase();

            db.StringIncrement("test:user:count");
        }

        [TestMethod]
        public void String_Test()
        {
            var db = GetDatabase();

            var a = db.StringSet("test:user:2:name", "软中华");

            var b = db.StringGetSet("test:user:2:name", "龙卷风");

            Console.WriteLine(string.Format("[{0}]", db.StringGet("test:user:2:name")));

            var products = new List<Product> {
                new Product{  SysNo = 1,ProductId = "P001", CreateTime = DateTime.Now, Price=1230 },
                new Product{  SysNo = 2,ProductId = "P002", CreateTime = DateTime.Now.AddSeconds(-123345), Price=543 },
                new Product{  SysNo = 3,ProductId = "P003", CreateTime = DateTime.Now.AddSeconds(-398450), Price=348 }
            };

            db.StringSet("test:products:json", JsonConvert.SerializeObject(products));

            var pstr = (db.StringGet("test:products:json"));

            foreach (var p in JsonConvert.DeserializeObject<List<Product>>(pstr))
            {
                Console.WriteLine(string.Format("SysNo:{0}, ProductId:{1}, CreateTime:{2}, Price:{3}", p.SysNo, p.ProductId, p.CreateTime, p.Price));
            }
        }

        [TestMethod]
        public void Hash_ComplexObject_Test()
        {
            var db = GetDatabase();

            var product = new Product
            {
                SysNo = 3,
                ProductId = "P003",
                Price = 8934,
                CreateTime = DateTime.Now
            };

            var hes = new List<HashEntry>();

            foreach (var p in product.GetType().GetProperties())
            {
                //Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(product));
                hes.Add(new HashEntry(p.Name, p.GetValue(product).ToString()));
            }

            db.HashSet("test:product:3:entity", hes.ToArray());
            //db.HashSet("test:product:1:entity", new HashEntry[] {
            //    new HashEntry("SysNo",1),
            //    new HashEntry("ProductId","P001"),
            //    new HashEntry("Price",1230),
            //    new HashEntry("CreateTime",DateTime.Now.Ticks)
            //});


            var ph = db.HashGetAll("test:product:3:entity");

            db.HashSet("test:product:2:entity", "SysNo", 2);
        }

        [TestMethod]
        public void Hash_ComplexObject_Update_Test()
        {
            var Product_3 = new
            {
                Name = "在这城市的生活里我总等待一个周末",
                ProductId = "P003-Update",
            };

            var hes = new List<HashEntry>();

            foreach (var p in Product_3.GetType().GetProperties())
            {
                hes.Add(new HashEntry(p.Name, p.GetValue(Product_3).ToString()));
                //Console.WriteLine(string.Format("key:{0}, val:{1}", p.Name, p.GetValue(product).ToString()));
            }

            var db = GetDatabase();

            db.HashSet("test:product:3:entity", hes.ToArray());
        }

        enum HE : byte
        {
            A,
            B,
        }

        [TestMethod]
        public void RedisCacheManager_Set_Test()
        {
            //var product = new Product
            //{
            //    SysNo = 5,
            //    ProductId = "P005",
            //    Price = 5,
            //    CreateTime = DateTime.Now
            //};

            //var _db = GetDatabase();

            //for (int i = 0; i < 600000; i++)
            //{
            //    product.SysNo = i;
            //    product.ProductId = "P" + i;
            //    product.Price = i;

            //    //var hes = new List<HashEntry>();
            //    //hes.Add(new HashEntry("SysNo", product.SysNo));
            //    //hes.Add(new HashEntry("ProductId", product.ProductId));
            //    //hes.Add(new HashEntry("Price", product.Price.ToString()));
            //    //hes.Add(new HashEntry("CreateTime", product.CreateTime.ToString()));

            //    //_db.HashSet("test:product:batch:" + i + ":entity", hes.ToArray());

            //    Set("test:product:batch:" + i + ":entity", product);
            //}

            for (int i = 0; i < 100000; i++)
            {
                Set("test:val:batch:" + i, RandomHelper.GenNumber(0, int.MaxValue));
            }
        }

        [TestMethod]
        public void 用户类型_属性为用户类型()
        {
            var c = new Company
            {
                Id = 1,
                Name = "JIF Software HighTech Company.",
                Product = new Product
                {
                    SysNo = 1,
                    ProductId = "P-001",
                    Price = 1,
                    CreateTime = DateTime.Now
                }
            };

            Set("test:usertype:property:model", c);

            //var cg = Get<Company>("test:usertype:property:model");
            //Console.WriteLine(JsonConvert.SerializeObject(cg));
        }

        Dictionary<Type, PropertyInfo[]> typePropertyCache = new Dictionary<Type, PropertyInfo[]>();

        [TestMethod]
        public void RedisCacheManager_Get_Test()
        {
            Console.WriteLine(Get<int>("test:val:batch:120"));
            //Console.WriteLine(Convert.ToDateTime("2017/10/19 22:53:02"));

            //var product = Get<Product>("test:product:batch:523348:entity");

            //Console.WriteLine();

            //Console.WriteLine(JsonConvert.SerializeObject(product));
        }

        private void Set<T>(string key, T data, int? cacheTimeStamp = null)
        {
            var _db = GetDatabase();

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

        private T Get<T>(string key)
        {
            var _db = GetDatabase();

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


        [TestMethod]
        public void Pattern_Test()
        {
            var _db = GetDatabase();

            for (int i = 0; i < 10000; i++)
            {
                _db.StringSet($"stu:{i}:info", i);

                _db.StringSet($"person:{RandomHelper.GenString(RandomHelper.CharSchemeEnum.NumChar, 30, 50)}:info", i);
            }
        }
    }

    class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Product Product { get; set; }
    }
}
