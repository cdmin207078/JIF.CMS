﻿using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Services.Articles.Dtos;
using System.Collections.Generic;

namespace JIF.CMS.Services.Articles
{
    public interface IArticleService
    {
        /// <summary>
        /// 获取 所有tags 列表
        /// </summary>
        /// <returns></returns>
        List<Tag> GetTags();

        /// <summary>
        /// 获取 tags 字典 , key : name, val : id
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetTagsDict();

        /// <summary>
        /// 获取文章具有tags
        /// </summary>
        /// <param name="id">文章系统编号</param>
        /// <returns></returns>
        List<Tag> GetArticleTags(int id);

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
        void DeleteArticle(int id);

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
        IPagedList<SearchArticleListOutput> GetArticles(string q, bool isDeleted = false, int pageIndex = 1, int pageSize = int.MaxValue);

        #endregion

        #region ArticleCategory

        void Insert(InsertArticleCategoryInput model);

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id">分类名称</param>
        /// <param name="forcedelartcle">是否同时删除所属分类下的文章</param>
        void DeleteArticleCategory(int id);

        void Update(int id, InsertArticleCategoryInput model);

        /// <summary>
        /// 获取具体文章分类信息
        /// </summary>
        /// <returns></returns>
        ArticleCategory GetCategory(int id);

        /// <summary>
        /// 获取所有文章分类列表
        /// </summary>
        /// <returns></returns>
        List<ArticleCategory> GetCategories();

        /// <summary>
        /// 获取所有文章分类, 转换为层级结构
        /// </summary>
        /// <remarks>key : level, value : categories</remarks>
        Dictionary<int, List<ArticleCategory>> GetCategoriesLevelDict();

        /// <summary>
        /// 获取所有文章分类, 对象关系关系树结构.
        /// </summary>
        /// <returns>返回顶层节点, 分支使用各个节点依次访问</returns>
        List<ArticleCategory> GetCategoriesTreeRelation();

        /// <summary>
        /// 获取所有文章分类, 对象树先序遍历
        /// </summary>
        List<TreeRelationObjectTraverseWrapper<ArticleCategory>> GetCategoriesSortArray();


        /// <summary>
        /// 保存分类层级关系
        /// </summary>
        /// <param name="categories"></param>
        void SaveCategoriesSort(List<SaveCategoriesSortInput> sorts);

        #endregion
    }
}
