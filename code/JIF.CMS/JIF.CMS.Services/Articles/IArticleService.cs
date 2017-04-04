using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services.Articles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles
{
    public interface IArticleService
    {
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Insert(CreateArticleInputDto model);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        void Delete(DeleteArticleDto model);


        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(UpdateArticleInputDto model);

        Article GetArticle(int id);

        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="s">搜索关键字 {账号 / Email / 电话}</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        IPagedList<ArticleSearchListOutDto> GetArticles(string q, int pageIndex = 1, int pageSize = int.MaxValue);

        void Insert(ArticleCategory model);

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id">分类名称</param>
        /// <param name="forcedelartcle">是否同时删除所属分类下的文章</param>
        void Delete(DeleteArticleCategoryDto model);

        void Update(ArticleCategory model);

        /// <summary>
        /// 获取具体文章分类信息
        /// </summary>
        /// <returns></returns>
        ArticleCategory GetCategory(int id);

        IEnumerable<ArticleCategory> GetCategories();
    }
}
