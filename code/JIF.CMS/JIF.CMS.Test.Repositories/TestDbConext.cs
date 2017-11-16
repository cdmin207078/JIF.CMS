using JIF.CMS.Test.Repositories.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Test.Repositories
{
    public class TestDbConext : DbContext
    {
        public TestDbConext(string connectionstring)
            : base(connectionstring)
        { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("user_info").HasKey(d => d.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
