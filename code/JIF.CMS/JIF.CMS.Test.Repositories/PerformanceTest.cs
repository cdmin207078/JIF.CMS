using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using JIF.CMS.Test.Repositories.Domain;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Collections.Generic;
using System.Threading.Tasks;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Test.Repositories
{
    [TestClass]
    public class PerformanceTest
    {
        /// <summary>
        /// database connection string
        /// </summary>
        //private readonly string connectionstring = "Data Source=192.168.0.130;Initial Catalog=RepositoriesPerformanceTestDB;User ID=sa;Password=eva349121171@;";
        private readonly string connectionstring = "Data Source=43.247.90.5;Initial Catalog=RepositoriesPerformanceTestDB;User ID=sa;Password=d4ihXjQLg3w6xn2IB5XM;";

        private List<UserInfo> GenSomeUsers()
        {
            var users = new List<UserInfo>();
            var names = RandomHelper.GenChinesePersonName(10000);

            for (int i = 0; i < 10000; i++)
            {
                var u = new UserInfo
                {
                    Name = "",
                    Gender = (UserInfo.GenderEnum)new Random(1).Next(0, 2),
                    Motto = "言葉にするのが下手な あなたの性格わかるから 信じてみて",
                    CreateTime = DateTime.Now,
                };

                users.Add(u);
            }

            return users;
        }

        /// <summary>
        /// 获取ef Context
        /// </summary>
        /// <param name="dbContext"></param>
        private DbContext getEfDbContext()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            // 初始化
            var dbContext = new RepositoriesPerformanceTestDbConext(connectionstring);

            // 预热
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingCollection.GenerateViews(new List<EdmSchemaError>());

            watch.Show("EF 预热");

            return dbContext;
        }

        [TestMethod]
        public void EF_Insert_Test()
        {
            var db = getEfDbContext();


            Stopwatch watch = new Stopwatch();
            watch.Start();

            var users = GenSomeUsers();
            watch.Show("生成用户");

            db.Set<UserInfo>().AddRange(users);
            db.SaveChanges();
            watch.Show("插入用户");
        }

        [TestMethod]
        public void EF_Query_Test()
        {
            var db = getEfDbContext();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            var users1 = db.Set<UserInfo>().ToList();
            watch.Show("查询所有不加条件");

            var users2 = db.Set<UserInfo>().Where(d => d.CreateTime > DateTime.Now).ToList();
            watch.Show("以时间为单一条件");

            var users3 = db.Set<UserInfo>().Where(d => d.Id > 100).ToList();
            watch.Show("以Id为条件");
        }

        [TestMethod]
        public void Dapper_Insert_Test()
        {
            IDbConnection db = new SqlConnection(connectionstring);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            var users = GenSomeUsers();
            watch.Show("生成用户");

            db.Insert(users);
            watch.Show("插入用户");
        }

        [TestMethod]
        public void Dapper_Query_Test()
        {
            IDbConnection db = new SqlConnection(connectionstring);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            var users1 = db.Query<UserInfo>("select * from user_info").ToList();
            watch.Show("查询所有不加条件");


            var users2 = db.Query<UserInfo>("select * from user_info where createTime > getdate()").ToList();
            watch.Show("以时间为单一条件");

            var users3 = db.Query<UserInfo>("select * from user_info where id > 100").ToList();
            watch.Show("以Id为条件");
        }
    }
}
