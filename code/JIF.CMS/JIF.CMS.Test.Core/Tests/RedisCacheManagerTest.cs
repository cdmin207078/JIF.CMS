using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Redis;
using JIF.CMS.Core.Configuration;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class RedisCacheManagerTest
    {
        private RedisCacheManager GetCacheManager()
        {
            var config = new JIFConfig.RedisConfiguration
            {
                Enabled = true,
                Server = "127.0.0.1:10082"
            };

            return new RedisCacheManager(config);
        }

        [TestMethod]
        public void String_Test()
        {
            var redis = GetCacheManager();

            var name = "中文";
            redis.Set("name", name);
            Console.WriteLine(redis.Get<string>("name"));

            var val = redis.Get<string>("baiyun");
            Console.WriteLine(string.IsNullOrWhiteSpace(val));
            Console.WriteLine(val);
        }

        [TestMethod]
        public void Number_Test()
        {
            var redis = GetCacheManager();

            int score = 100;
            redis.Set("score", score);
            Console.WriteLine(redis.Get<string>("score"));

            decimal amount = 100.0M;
            redis.Set("amount", amount);
            Console.WriteLine(redis.Get<decimal>("amount"));

            long total = 100000000000000;
            redis.Set("total", total);
            Console.WriteLine(redis.Get<long>("total"));

            double money = 1000000000000.123112d;
            redis.Set("money", money);
            Console.WriteLine(redis.Get<double>("money"));
        }

        [TestMethod]
        public void DateTime_Test()
        {
            var redis = GetCacheManager();

            var now = DateTime.Now;
            redis.Set("now", now);
            Console.WriteLine(redis.Get<DateTime>("now"));
        }

        [TestMethod]
        public void Boolean_Test()
        {
            var redis = GetCacheManager();

            var success = false;
            redis.Set("success", success);
            Console.WriteLine(redis.Get<bool>("success"));

            // 存入数值, 返回 bool 报错
            //int fail = 0;
            //redis.Set("fail", fail);
            //Console.WriteLine(redis.Get<bool>("fail"));
        }

        [TestMethod]
        public void Enum_Test()
        {

            var redis = GetCacheManager();

            var RCMTE = RCMTEnum.你;

            redis.Set("RCMTE", RCMTE);

            var val = redis.Get<RCMTEnum>("RCMTE");

            var r = redis.Get<RCMTEnum?>("RCMTESSS");
            Console.WriteLine(r.HasValue);
            Console.WriteLine(r);
        }

        [TestMethod]
        public void IEnumerable_Test()
        {
            var redis = GetCacheManager();

            var names = new List<string>
            {
                "A",
                "B",
                "你好",
                "Sakura"
            };

            redis.Set("names", names);
            var retNames = redis.Get<List<string>>("names");
            Console.WriteLine(JsonConvert.SerializeObject(retNames));

            var ages = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            redis.Set("ages", ages);
            var retAges = redis.Get<int[]>("ages");
            Console.WriteLine(JsonConvert.SerializeObject(retAges));

            // 查找不存在的数据
            var agessss = redis.Get<int[]>("agessss");
            if (agessss == null)
            {
                Console.WriteLine("agessss is null");
            }
            else
            {
                Console.WriteLine(JsonConvert.SerializeObject(agessss));
            }
        }

        [TestMethod]
        public void Complex_Test()
        {
            var redis = GetCacheManager();

            var book = new Book
            {
                Id = 1,
                Title = "天雨流芳",
                Description = "“天雨流芳”是纳西语音译语，行云流水般书写在丽江木府旁的一座牌坊上，其纳西语音意为“读书去吧”。汉纳两语，一音双意，写下这四个字的读书人，对读书之趣味领悟到如此境界，实在令人惊叹不已。的确，读一部好书，对我们心智的滋养，就像天雨之于谷物的成熟，让你的心灵沐浴着智慧的浩荡清香，而感觉无比的幸福和安详。作为世界文化遗产的丽江古城，就是这样的一部好书。",
                IsOutStock = true,
                PublishDate = new DateTime(2010, 1, 4)
            };

            redis.Set("book:1:info", book);

            var retBook = redis.Get<Book>("book:1:info");

            Console.WriteLine(JsonConvert.SerializeObject(retBook));


            var books = new List<Book>
            {
                new Book {
                    Id = 1,
                    Title = "天雨流芳",
                    Description = "“天雨流芳”是纳西语音译语，行云流水般书写在丽江木府旁的一座牌坊上，其纳西语音意为“读书去吧”。汉纳两语，一音双意，写下这四个字的读书人，对读书之趣味领悟到如此境界，实在令人惊叹不已。的确，读一部好书，对我们心智的滋养，就像天雨之于谷物的成熟，让你的心灵沐浴着智慧的浩荡清香，而感觉无比的幸福和安详。作为世界文化遗产的丽江古城，就是这样的一部好书。",
                    IsOutStock = true,
                    PublishDate = new DateTime(2010, 1, 4),
                    RCMT = RCMTEnum.A,
                },
                new Book {
                    Id = 2,
                    Title = "22222222222222222222",
                    Description = "22222222222222222222",
                    IsOutStock = true,
                    PublishDate = new DateTime(2018, 1, 4),
                    RCMT = RCMTEnum.你,
                },
                new Book {
                    Id = 3,
                    Title = "3333333333333333",
                    Description = "3333333333333333",
                    IsOutStock = true,
                    PublishDate = new DateTime(2018, 2, 22),
                    RCMT = RCMTEnum.の,
                },
            };

            redis.Set("books", books);

            var retBooks = redis.Get<List<Book>>("books");

            Console.WriteLine(JsonConvert.SerializeObject(retBooks));
        }

        [TestMethod]
        public void Huge_Data_Test()
        {
            var count = 100 * 100 * 100 * 2; // 200w
            count = 100;

            var redis = GetCacheManager();

            for (int i = 0; i < count; i++)
            {
                redis.Set(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
        }

        [TestMethod]
        public void Redis_Cluster_Test()
        {

        }

        enum RCMTEnum
        {
            A,
            B,
            你,
            我,
            の
        }

        class Book
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public DateTime PublishDate { get; set; }

            public bool IsOutStock { get; set; }

            public RCMTEnum RCMT { get; set; }
        }

    }
}
