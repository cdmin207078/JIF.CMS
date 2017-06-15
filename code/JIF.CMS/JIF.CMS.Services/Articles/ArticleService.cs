using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Services.Articles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JIF.CMS.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<ArticleCategory> _articleCategoryRepository;
        private readonly IRepository<SysAdmin> _sysAdminRepository;

        private readonly IWorkContext _workContext;

        public ArticleService(IRepository<Article> articleRepository,
            IRepository<ArticleCategory> articleCategoryRepository,
            IRepository<SysAdmin> sysAdminRepository,
            IWorkContext workContext)
        {
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _sysAdminRepository = sysAdminRepository;
            _workContext = workContext;
        }

        public void Insert(InsertArticleInput model)
        {
            if (string.IsNullOrWhiteSpace(model.Title)
                || string.IsNullOrWhiteSpace(model.MarkdownContent))
            {
                throw new JIFException("文章 标题 / 内容 不能为空");
            }

            var entity = new Article
            {
                Title = model.Title,
                MarkdownContent = model.MarkdownContent,
                CategoryId = model.CategoryId,
                AllowComments = model.AllowComments,
                Published = model.Published,
                CreateTime = DateTime.Now,
                CreateUserId = _workContext.CurrentUser.Id
            };

            _articleRepository.Insert(entity);

        }

        public void Delete(DeleteArticleInput model)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, InsertArticleInput model)
        {
            if (string.IsNullOrWhiteSpace(model.Title)
                || string.IsNullOrWhiteSpace(model.MarkdownContent))
            {
                throw new JIFException("文章 标题 / 内容 不能为空");
            }

            var entity = GetArticle(id);

            if (entity == null)
            {
                throw new JIFException("欲修改文章不存在");
            }

            entity.Title = model.Title;
            //entity.Content = model.Content;
            entity.MarkdownContent = model.MarkdownContent;
            entity.CategoryId = model.CategoryId;
            entity.AllowComments = model.AllowComments;
            entity.Published = model.Published;
            entity.IsDeleted = model.IsDeleted;
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUserId = _workContext.CurrentUser.Id;

            _articleRepository.Update(entity);
        }

        public Article GetArticle(int id)
        {
            return _articleRepository.Get(id);
        }

        public IPagedList<SearchArticleListOutput> GetArticles(string q, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = from article in _articleRepository.Table
                        join sysadmin in _sysAdminRepository.Table on article.CreateUserId equals sysadmin.Id
                        join category in _articleCategoryRepository.Table on article.CategoryId equals category.Id
                        select new SearchArticleListOutput
                        {
                            Id = article.Id,
                            Title = article.Title,
                            Author = sysadmin.Account,
                            Category = category.Name,
                            CreateTime = article.CreateTime
                        };

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(d => d.Title.ToLower().Contains(q.ToLower()));
            }

            return new PagedList<SearchArticleListOutput>(query.OrderByDescending(d => d.Id), pageIndex, pageSize);
        }



        public void Insert(ArticleCategory model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new JIFException("分类名称不能为空");
            }

            if (_articleCategoryRepository.Table.Any(d => d.Name == model.Name && d.Id != model.Id))
            {
                throw new JIFException("分类名称已经存在");
            }

            _articleCategoryRepository.Insert(model);
        }

        public void Delete(DeleteArticleCategoryInput model)
        {
            throw new NotImplementedException();
        }

        public void Update(ArticleCategory model)
        {
            throw new NotImplementedException();
        }

        public ArticleCategory GetCategory(int id)
        {
            return _articleCategoryRepository.Get(id);
        }

        public IEnumerable<ArticleCategory> GetCategories()
        {
            return _articleCategoryRepository.Table.OrderByDescending(d => d.Order).ToList();
        }
    }
}
