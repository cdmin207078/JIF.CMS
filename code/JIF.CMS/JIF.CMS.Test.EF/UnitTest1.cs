using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Data.EntityFramework;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Core.Data;
using System.Linq;
using System.Data.Entity.Core.EntityClient;

namespace JIF.CMS.Test.EF
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var builder = new EntityConnectionStringBuilder();
            builder.ProviderConnectionString = "Data Source=192.168.0.120;port=3306;Initial Catalog=jif.cms;uid=root;pwd=123456";
            builder.Provider = "MySql.Data.MySqlClient";

            var db = new JIFDbContext("JIF.CMS.DB");


            IRepository<Article> _articleRepository = new EfRepository<Article>(db);
            IRepository<ArticleCategory> _articleCategoryRepository = new EfRepository<ArticleCategory>(db);



            var categories = _articleCategoryRepository.Table.ToList();
            var articles = _articleRepository.Table.ToList();



        }
    }
}
