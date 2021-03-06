﻿using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Domain.Articles;
using System.Data.Entity;

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
            modelBuilder.Entity<Article>().ToTable("articles").HasKey(d => d.Id);
            modelBuilder.Entity<ArticleCategory>().ToTable("article_categories").HasKey(d => d.Id).Ignore(d => d.Parent).Ignore(d => d.Childs);
            modelBuilder.Entity<Tag>().ToTable("tags").HasKey(d => d.Id);
            modelBuilder.Entity<ArticleTag>().ToTable("article_tags").HasKey(d => d.Id);
            modelBuilder.Entity<Attachment>().ToTable("attachments").HasKey(d => d.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
