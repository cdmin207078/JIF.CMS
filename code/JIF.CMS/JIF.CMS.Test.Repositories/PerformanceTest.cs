﻿using System;
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
using JIF.CMS.Core.Extensions;
using Common.Logging;
using System.Transactions;
using Newtonsoft.Json;

namespace JIF.CMS.Test.Repositories
{
    [TestClass]
    public class PerformanceTest
    {
        /// <summary>
        /// database connection string
        /// </summary>
        //private readonly string connectionstring = "Data Source=192.168.0.130;Initial Catalog=RepositoriesPerformanceTestDB;User ID=sa;Password=eva349121171@;";
        private readonly string connectionstring = "Data Source=43.247.90.5;Initial Catalog=RepositoriesPerformanceTestDB;User ID=sa;Password=d4ihXjQLg3w6xn2IB5XM;MultipleActiveResultSets=true";

        private List<UserInfo> GenSomeUsers(int count)
        {
            var users = new List<UserInfo>();
            var names = RandomHelper.GenChinesePersonName(count);

            for (int i = 0; i < count; i++)
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
        public void Insert_Performance_Test()
        {
            var ef = getEfDbContext();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            var users = GenSomeUsers(10000);
            watch.Show("生成用户");

            ef.Set<UserInfo>().AddRange(users);
            ef.SaveChanges();
            watch.Show("ef 插入用户");

            IDbConnection dap = new SqlConnection(connectionstring);

            dap.Insert(users);
            watch.Show("dapper 插入用户");
        }

        [TestMethod]
        public void Query_Performance_Test()
        {
            var ef = getEfDbContext();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            var u1 = ef.Set<UserInfo>().ToList();
            watch.Show("ef 查询所有不加条件");

            var u2 = ef.Set<UserInfo>().Where(d => d.CreateTime > DateTime.Now).ToList();
            watch.Show("ef 以时间为单一条件");

            var u3 = ef.Set<UserInfo>().Where(d => d.Id > 100).ToList();
            watch.Show("ef 以Id为条件");



            IDbConnection dap = new SqlConnection(connectionstring);

            var u4 = dap.Query<UserInfo>("select * from user_info").ToList();
            watch.Show("查询所有不加条件");

            var u5 = dap.Query<UserInfo>("select * from user_info where createTime > getdate()").ToList();
            watch.Show("以时间为单一条件");

            var u6 = dap.Query<UserInfo>("select * from user_info where id > 100").ToList();
            watch.Show("以Id为条件");
        }

        [TestMethod]
        public void IDbConnection_Using_New_Test()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 100; i++)
            {
                IDbConnection connection = new SqlConnection(connectionstring);
                connection.ExecuteScalar<int>("select count(1) from user_info;");
            }
            watch.Show("每次new 新的connection");

            for (int i = 0; i < 100; i++)
            {
                using (IDbConnection connection = new SqlConnection(connectionstring))
                {
                    connection.ExecuteScalar<int>("select count(1) from user_info;");
                }
            }
            watch.Show("每次new 新的connection + using");

            IDbConnection shareConnection = new SqlConnection(connectionstring);
            for (int i = 0; i < 100; i++)
            {
                shareConnection.ExecuteScalar<int>("select count(1) from user_info;");
            }
            watch.Show("共用 connection");
        }

        [TestMethod]
        public async Task IDbConnection_Async_Multi_Test()
        {
            var logger = LogManager.GetLogger("");
            IDbConnection shareConnection = new SqlConnection(connectionstring);

            var tasks = new List<Task>();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    // wrong: System.InvalidOperationException: 连接未关闭。 连接的当前状态为正在连接。
                    //var count = shareConnection.ExecuteScalar<int>("select count(1) from user_info;");
                    //logger.Info(string.Format("count: {0}, thread: {1}", count, Thread.CurrentThread.ManagedThreadId));

                    using (IDbConnection unitConnection = new SqlConnection(connectionstring))
                    {
                        var count = unitConnection.ExecuteScalar<int>("select count(1) from user_info;");
                        logger.Info(string.Format("count: {0}, thread: {1}", count, Thread.CurrentThread.ManagedThreadId));
                    }
                }));
            }
            await Task.WhenAll(tasks);
            watch.Show("100 个线程同时访问");

            tasks.Clear();

            // wrong: System.InvalidOperationException: 无效操作。连接被关闭.
            // note: 在使用中 不可使用同一dbconnection 来执行异步任务等待.
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(shareConnection.ExecuteScalarAsync<int>("select count(1) from user_info;"));
            }
            await Task.WhenAll(tasks);
            watch.Show("100 个异步方法 - ExecuteScalarAsync 执行");

            for (int i = 0; i < 100; i++)
            {
                var count = shareConnection.ExecuteScalar<int>("select count(1) from user_info;");
                logger.Info(string.Format("count: {0}, thread: {1}", count, Thread.CurrentThread.ManagedThreadId));
            }

            watch.Show("同步执行100次");

        }

        [TestMethod]
        public async Task Dapper_Async_Methods_Test()
        {
            IDbConnection shareConnection = new SqlConnection(connectionstring);

            var c1 = shareConnection.ExecuteScalarAsync<int>("select count(1) from user_info;");
            var c2 = shareConnection.ExecuteScalarAsync<int>("select count(1) from user_info;");
            var c3 = shareConnection.ExecuteScalarAsync<int>("select count(1) from user_info;");
            var c4 = shareConnection.ExecuteScalarAsync<int>("select count(1) from user_info;");

            await (c1);
            await (c2);
            await (c3);
            await (c4);
        }

        #region TransactionScope 事务测试

        [TestMethod]
        public void Dapper_TransactionScope_Test()
        {
            var watch = new Stopwatch();
            watch.Start();

            TransactionOptions option = new TransactionOptions();
            option.Timeout = new TimeSpan(0, 0, 60); // 事务超时时间, 默认设置为 1min.
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope scope = new TransactionScope())
            {
                Console.WriteLine("外层事务信息: " + JsonConvert.SerializeObject(Transaction.Current.TransactionInformation));

                // 内部包含已经提交的事务
                update_user_name_motto_with_transaction();

                update_user_name();
                update_user_motto();

                update_user_name_diffent_database();

                update_user_fail();

                scope.Complete();
            }
            watch.Show("使用事务");

            update_user_name();
            update_user_motto();
            watch.Show("未使用事务");
        }

        private void update_user_name()
        {
            IDbConnection dap = new SqlConnection(connectionstring);
            var sql = "update user_info set Name = @Name where id = @Id";
            dap.Execute(sql, new { Id = 1, Name = "望乡" });
        }

        private void update_user_motto()
        {
            IDbConnection dap = new SqlConnection(connectionstring);
            var sql = "update user_info set Motto = @Motto where id = @Id";
            dap.Execute(sql, new { Id = 1, Motto = "窗外明月光 映照我脸庞 月之故乡亲人是否安康" });
        }

        private void update_user_name_motto_with_transaction()
        {
            IDbConnection dap = new SqlConnection(connectionstring);

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                Console.WriteLine("内层事务信息: " + JsonConvert.SerializeObject(Transaction.Current.TransactionInformation));

                var sql = "update user_info set Name = @Name where id = @Id";
                dap.Execute(sql, new { Id = 1, Name = "相思" });

                sql = "update user_info set Motto = @Motto where id = @Id";
                dap.Execute(sql, new { Id = 1, Motto = "红豆生南国 春来发几枝 愿君多采撷 此物最相思" });

                sql = "update user_info set Gender = @Gender where id = @Id";
                dap.Execute(sql, new { Id = 1, Gender = 1 });

                scope.Complete();
            }
        }

        private void update_user_fail()
        {
            throw new Exception("发生错误");
        }

        private void update_user_name_diffent_database()
        {
            IDbConnection dap = new SqlConnection("Data Source=43.247.90.5;Initial Catalog=YPW;User ID=sa;Password=d4ihXjQLg3w6xn2IB5XM;MultipleActiveResultSets=true");

            var sql = "update user_info set name = @Name where id = @Id";
            dap.Execute(sql, new { Id = 338, Name = "相思" });
        }

        #endregion


        [TestMethod]
        public void MyTestMethod()
        {
            IDbConnection dap = new SqlConnection(connectionstring);

            var user = dap.QueryFirst<UserInfo>("select * from user_info");

            Console.WriteLine(JsonConvert.SerializeObject(user));
        }
    }
}

