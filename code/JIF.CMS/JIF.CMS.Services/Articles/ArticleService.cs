using JIF.CMS.Core.Data;
using JIF.CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core;
using JIF.CMS.Services.Articles.Dtos;

namespace JIF.CMS.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;

        public ArticleService(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public void Add(ArticleInertDto model)
        {
            throw new NotImplementedException();
        }

        public Article Get(int id)
        {
            throw new NotImplementedException();
        }

        public IPagedList<ArticleSearchListOutDto> Load(string q, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var articles = new List<ArticleSearchListOutDto>();

            for (int i = 1; i < 100; i++)
            {
                articles.Add(new ArticleSearchListOutDto { Id = 1, Title = "VueJs - 初见", Author = "admin", Category = "程序开发", CreateTime = DateTime.Now });
                articles.Add(new ArticleSearchListOutDto { Id = 2, Title = "phpstudy 访问速度慢解决办法", Author = "admin", Category = "程序开发", CreateTime = DateTime.Now });
                articles.Add(new ArticleSearchListOutDto { Id = 3, Title = "autofac 循环依赖处理", Author = "admin", Category = "程序开发", CreateTime = DateTime.Now });
                articles.Add(new ArticleSearchListOutDto { Id = 4, Title = "C# Enum - String - int 互相转换", Author = "admin", Category = "程序开发", CreateTime = DateTime.Now });
                articles.Add(new ArticleSearchListOutDto { Id = 5, Title = "typecho 安装访问端口设置", Author = "admin", Category = "程序开发", CreateTime = DateTime.Now });
            }

            q = q.Trim();

            return new PagedList<ArticleSearchListOutDto>(articles.Where(d => d.Title.Contains(q)).ToList(), pageIndex, pageSize);
        }
    }
}
