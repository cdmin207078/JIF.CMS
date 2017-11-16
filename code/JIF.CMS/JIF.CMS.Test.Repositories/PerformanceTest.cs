using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using JIF.CMS.Test.Repositories.Domain;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace JIF.CMS.Test.Repositories
{
    [TestClass]
    public class PerformanceTest
    {
        /// <summary>
        /// database connection string
        /// </summary>
        private readonly string connectionstring = "Data Source=192.168.0.130;Initial Catalog=RepositoriesPerformanceTestDB;User ID=sa;Password=eva349121171@;";

        [TestMethod]
        public void EF_Test()
        {
            var db = new TestDbConext(connectionstring);

            for (int i = 0; i < 10000; i++)
            {
                var u = new UserInfo
                {
                    //Id = i,
                    Name = "用户:" + i,
                    CreateTime = DateTime.Now,
                    Gender = UserInfo.GenderEnum.Male,
                    Motto = "个性签名"
                };

                db.Set<UserInfo>().Add(u);
                db.SaveChanges();
            }


            var users = db.Set<UserInfo>().ToList();
        }

        private void ShowTime(long ticks)
        {
            Console.WriteLine(string.Format("耗时: {0}ms", ticks.ToString()));
        }

        [TestMethod]
        public void Query_Test()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            var db = new TestDbConext(connectionstring);
            sw.Stop();

            Console.WriteLine("------------ 初始化 -------------");
            ShowTime(sw.ElapsedMilliseconds);


            sw.Restart();
            var users = db.Set<UserInfo>().ToList();
            sw.Stop();

            Console.WriteLine("------------ 查询 -------------");
            ShowTime(sw.ElapsedMilliseconds);
        }
    }
}
