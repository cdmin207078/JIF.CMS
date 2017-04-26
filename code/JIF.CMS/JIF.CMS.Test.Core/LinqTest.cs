using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JIF.CMS.Test.Core
{
    [TestClass]
    public class LinqTest
    {
        [TestMethod]
        public void Linq_RowData_Col_CuttingMulti_Test()
        {
            var xc = new Person
            {
                Id = 1,
                Name = "小崔",
                Hobbies = "编程,音乐,把妹",
                Age = 20
            };
            var lk = new Person
            {
                Id = 1,
                Name = "凯哥",
                Hobbies = "Coding,Music,Girl",
                Age = 18
            };

            var source = new List<Person> { xc, lk };

            Console.WriteLine("=======================原始数据=======================");
            foreach (var s in source)
            {
                Console.WriteLine(JsonConvert.SerializeObject(s));
            }

            Console.WriteLine("=======================处理之后=======================");

            var result = (from t in source
                          from c in t.Hobbies.Split(',')
                          select new Person() { Id = t.Id, Name = t.Name, Hobbies = c, Age = t.Age }).ToList();

            foreach (var s in result)
            {
                Console.WriteLine(JsonConvert.SerializeObject(s));
            }

        }
    }

    class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Hobbies { get; set; }

        public int Age { get; set; }
    }
}
