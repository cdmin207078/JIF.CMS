using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Core.Extensions;
using JIF.CMS.Core.Helpers;
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
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<ArticleTag> _articleTagRepository;

        private readonly IRepository<SysAdmin> _sysAdminRepository;


        private readonly IWorkContext _workContext;

        public ArticleService(IRepository<Article> articleRepository,
            IRepository<ArticleCategory> articleCategoryRepository,
            IRepository<Tag> tagRepository,
            IRepository<ArticleTag> articleTagRepository,
            IRepository<SysAdmin> sysAdminRepository,
            IWorkContext workContext)
        {
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
            _articleTagRepository = articleTagRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _sysAdminRepository = sysAdminRepository;
            _workContext = workContext;
        }

        #region Tags

        /// <summary>
        /// 获取 所有tags 列表
        /// </summary>
        /// <returns></returns>
        public List<Tag> GetTags()
        {
            return _tagRepository.Table.ToList();
        }

        /// <summary>
        /// 获取 tags 字典 , key : name, val : id
        /// </summary>
        /// <returns></returns>

        public Dictionary<string, int> GetTagsDict()
        {
            var tags = GetTags();

            if (tags.IsNullOrEmpty())
            {
                return new Dictionary<string, int>();
            }

            return tags.ToDictionary(k => k.Name, v => v.Id);
        }

        /// <summary>
        /// 获取文章具有tags
        /// </summary>
        /// <param name="id">文章系统编号</param>
        /// <returns></returns>
        public List<Tag> GetArticleTags(int id)
        {
            return (from a in _articleTagRepository.Table
                    join b in _tagRepository.Table on a.TagId equals b.Id
                    where a.ArticleId == id
                    select b).ToList();
        }

        /// <summary>
        /// 保存文章标签数据
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <param name="tags">标签</param>
        private void SaveArticleTags(int articleId, List<string> tags)
        {
            if (tags.IsNullOrEmpty())
            {
                var tagsDic = GetTagsDict();

                foreach (var t in tags)
                {
                    if (string.IsNullOrWhiteSpace(t))
                        continue;

                    if (tagsDic.ContainsKey(t))
                    {
                        _articleTagRepository.Insert(new ArticleTag { ArticleId = articleId, TagId = tagsDic[t] });
                    }
                    else
                    {
                        var tag = new Tag { Name = t };
                        _tagRepository.Insert(tag);

                        _articleTagRepository.Insert(new ArticleTag { ArticleId = articleId, TagId = tag.Id });
                    }
                }
            }
        }

        #endregion

        #region Article

        public void Insert(InsertArticleInput model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
                throw new JIFException("文章标题不能为空");

            if (string.IsNullOrWhiteSpace(model.Content) && string.IsNullOrWhiteSpace(model.MarkdownContent))
                throw new JIFException("文章内容不能为空");

            var article = new Article
            {
                Title = model.Title,
                MarkdownContent = model.MarkdownContent,
                Content = model.Content,
                PublishTime = model.PublishTime,
                CategoryId = model.CategoryId,
                AllowComments = model.AllowComments,
                IsPublished = model.IsPublished,
                CreateTime = DateTime.Now,
                CreateUserId = _workContext.CurrentUser.Id
            };

            _articleRepository.Insert(article);

            // 保存文章标签
            SaveArticleTags(article.Id, model.Tags);
        }

        public void DeleteArticle(int id)
        {
            var entity = _articleRepository.Get(id);
            if (entity == null)
            {
                throw new JIFException("文章不存在");
            }

            entity.IsDeleted = true;
            entity.UpdateUserId = _workContext.CurrentUser.Id;
            entity.UpdateTime = DateTime.Now;

            _articleRepository.Update(entity);
        }

        public void Update(int id, InsertArticleInput model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
                throw new JIFException("文章标题不能为空");


            if (string.IsNullOrWhiteSpace(model.Content) && string.IsNullOrWhiteSpace(model.MarkdownContent))
                throw new JIFException("文章内容不能为空");

            var entity = GetArticle(id);

            if (entity == null)
                throw new JIFException("欲修改文章不存在");

            entity.Title = model.Title;
            entity.MarkdownContent = model.MarkdownContent;
            entity.Content = model.Content;
            entity.PublishTime = model.PublishTime;
            entity.CategoryId = model.CategoryId;
            entity.AllowComments = model.AllowComments;
            entity.IsPublished = model.IsPublished;
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUserId = _workContext.CurrentUser.Id;

            _articleRepository.Update(entity);

            // 保存文章标签, 先删除原有文章标签
            _articleTagRepository.Delete(_articleTagRepository.Table.Where(d => d.ArticleId == id));
            SaveArticleTags(id, model.Tags);
        }

        public Article GetArticle(int id)
        {
            return _articleRepository.Get(id);
        }

        public IPagedList<SearchArticleListOutput> GetArticles(string q, bool isDeleted = false, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = from article in _articleRepository.Table
                        join cu in _sysAdminRepository.Table on article.CreateUserId equals cu.Id
                        join uu in _sysAdminRepository.Table on article.UpdateUserId equals uu.Id into auu
                        from uu in auu.DefaultIfEmpty()
                        join category in _articleCategoryRepository.Table on article.CategoryId equals category.Id
                        where article.IsDeleted == isDeleted
                        select new SearchArticleListOutput
                        {
                            Id = article.Id,
                            Title = article.Title,
                            Author = cu.Account,
                            CreateTime = article.CreateTime,
                            Category = category.Name,
                            LastUpdateTime = article.UpdateTime.HasValue ? article.UpdateTime.Value : article.CreateTime,
                            LastUpdateUser = uu == null ? cu.Account : uu.Account,
                            IsPublished = article.IsPublished,
                            PublishTime = article.PublishTime
                        };

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(d => d.Title.ToLower().Contains(q.ToLower()));
            }

            return new PagedList<SearchArticleListOutput>(query.OrderByDescending(d => d.Id), pageIndex, pageSize);
        }

        #endregion

        #region Article Category

        /// <summary>
        /// 新增文章分类
        /// </summary>
        /// <param name="model"></param>
        public void Insert(InsertArticleCategoryInput model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new JIFException("分类名称不能为空");

            if (_articleCategoryRepository.Table.Any(d => d.Name == model.Name))
                throw new JIFException("分类名称已经存在");

            // 新增时排序默认排到所属分类最末
            var orderIndex = _articleCategoryRepository.Table.Where(d => d.ParentId == model.ParentId).Max(d => d.OrderIndex) + 1;

            var entity = new ArticleCategory
            {
                Name = model.Name,
                ParentId = model.ParentId,
                CoverImg = model.CoverImg,
                Description = model.Description,
                OrderIndex = orderIndex
            };

            _articleCategoryRepository.Insert(entity);
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id"></param>
        public void DeleteArticleCategory(int id)
        {
            var category = _articleCategoryRepository.Get(id);

            if (category == null)
                return;

            var articles = _articleRepository.Table.Where(d => d.CategoryId == id).ToList();
            if (!articles.IsNullOrEmpty())
            {
                articles.ForEach(d =>
                {
                    d.CategoryId = 0;
                });

                _articleRepository.Update(articles);
            }

            _articleCategoryRepository.Delete(category);
        }

        /// <summary>
        /// 修改分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void Update(int id, InsertArticleCategoryInput model)
        {
            if (_articleCategoryRepository.Table.Any(d => d.Name == model.Name && d.Id != id))
                throw new JIFException("分类名称已经存在");

            var entity = _articleCategoryRepository.Get(id);

            if (entity == null)
                throw new JIFException("分类不存在");

            // 若更改所属父级, 则组内排序初始化为组内末尾
            if (entity.ParentId != model.ParentId)
            {
                entity.OrderIndex = _articleCategoryRepository.Table.Where(d => d.ParentId == model.ParentId).Max(d => d.OrderIndex) + 1;
            }

            entity.Name = model.Name;
            entity.ParentId = model.ParentId;
            entity.CoverImg = model.CoverImg;
            entity.Description = model.Description;

            _articleCategoryRepository.Update(entity);
        }

        public ArticleCategory GetCategory(int id)
        {
            return _articleCategoryRepository.Get(id);
        }

        /// <summary>
        /// 获取所有文章分类列表
        /// </summary>
        /// <returns></returns>
        public List<ArticleCategory> GetCategories()
        {
            return _articleCategoryRepository.Table.ToList();
        }

        /// <summary>
        /// 获取所有文章分类, 转为 层级 - 数据源 对照字典
        /// </summary>
        /// <remarks>key : level, value : categories</remarks>
        public Dictionary<int, List<ArticleCategory>> GetCategoriesLevelDict()
        {
            var categories = GetCategories();
            if (categories.IsNullOrEmpty())
            {
                return new Dictionary<int, List<ArticleCategory>>();
            }

            // 层数
            var level = 1;

            var result = new Dictionary<int, List<ArticleCategory>>();

            var firstLevel = categories.Where(d => d.ParentId == 0).OrderBy(d => d.OrderIndex).ToList();

            result.Add(level, firstLevel);
            categories.RemoveAll(d => firstLevel.Any(l => l.Id == d.Id));

            while (true)
            {
                var parents = result.LastOrDefault().Value.Select(d => d.Id).ToList();

                var levels = categories.Where(d => parents.Contains(d.ParentId)).OrderBy(d => d.OrderIndex).ToList();
                if (levels.IsNullOrEmpty())
                    break;
                else
                {
                    level++;
                    result.Add(level, levels);
                    categories.RemoveAll(d => levels.Any(l => l.Id == d.Id));
                }

                if (categories.Any())
                    break;
            }

            return result;
        }

        /// <summary>
        /// 获取所有文章分类, 对象关系关系树结构. 
        /// </summary>
        /// <returns>只返回已排序的顶层节点, 分支使用各个节点依次访问</returns>
        public List<ArticleCategory> GetCategoriesTreeRelation()
        {


            return GetCategories().AsTreeRelation().ToList();
        }

        /// <summary>
        /// 获取所有文章分类, 对象树先序遍历
        /// </summary>
        public List<TreeRelationObjectTraverseWrapper<ArticleCategory>> GetCategoriesSortArray()
        {
            return GetCategories().GetTreeRelationPreorder().ToList();
        }

        /// <summary>
        /// 保存分类层级关系
        /// </summary>
        /// <param name="categories"></param>
        public void SaveCategoriesSort(List<SaveCagegoriesSortInput> sorts)
        {
            var categories = _articleCategoryRepository.Table.ToDictionary(d => d.Id);
            recursiveUpdateCategoryRelations(categories, sorts, 0);
            _articleCategoryRepository.Update(categories.Values);
        }

        /// <summary>
        /// 递归设置分类所属父级 & 排序
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="sorts"></param>
        /// <param name="parentId"></param>
        private void recursiveUpdateCategoryRelations(Dictionary<int, ArticleCategory> categories, List<SaveCagegoriesSortInput> sorts, int parentId)
        {
            if (categories.IsNullOrEmpty() || sorts.IsNullOrEmpty())
                return;

            for (int i = 0; i < sorts.Count; i++)
            {
                var sort = sorts[i];
                var category = categories[sort.Id];

                category.ParentId = parentId;
                category.OrderIndex = i;

                recursiveUpdateCategoryRelations(categories, sort.Children, sort.Id);
            }
        }
        #endregion
    }
}
