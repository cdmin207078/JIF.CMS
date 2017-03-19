using JIF.CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Data.EntityFramework
{
    public class JIFDbContext : DbContext
    {
        public JIFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysAdmin>().ToTable("sys_admin").HasKey(d => d.Id);
            modelBuilder.Entity<Article>().ToTable("Article").HasKey(d => d.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
