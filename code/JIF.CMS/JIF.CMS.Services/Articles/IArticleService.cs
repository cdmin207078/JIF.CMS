using JIF.CMS.Core;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Services.Articles.Dtos;
using System.Collections.Generic;

namespace JIF.CMS.Services.Articles
{
    public interface IArticleService
    {
        #region Artilce

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Insert(InsertArticleInput model);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        void Delete(DeleteArticleInput model);

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(int id, InsertArticleInput model);

        Article GetArticle(int id);

        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="s">搜索关键字 {账号 / Email / 电话}</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        IPagedList<SearchArticleListOutput> GetArticles(string q, int pageIndex = 1, int pageSize = int.MaxValue);

        #endregion

        #region ArticleCategory

        void Insert(ArticleCategory model);

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id">分类名称</param>
        /// <param name="forcedelartcle">是否同时删除所属分类下的文章</param>
        void Delete(DeleteArticleCategoryInput model);

        void Update(ArticleCategory model);

        /// <summary>
        /// 获取具体文章分类信息
        /// </summary>
        /// <returns></returns>
        ArticleCategory GetCategory(int id);

        List<ArticleCategory> GetCategories();

        #endregion
    }
}
