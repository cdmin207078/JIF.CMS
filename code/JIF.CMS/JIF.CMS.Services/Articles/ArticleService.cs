using JIF.CMS.Core.Data;
using JIF.CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;

        public ArticleService(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public string Sayhello()
        {
            return "Merry Christmas Mr.Lawrence";
        }
    }
}
