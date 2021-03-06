﻿using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Services;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Articles.Dtos;
using JIF.CMS.WebApi.Framework.Controllers;
using System.Net.Http;
using System.Web.Http;

namespace JIF.CMS.Management.API.Controllers
{
    public class ArticleController : AdminBaseController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [WebApi.Framework.Filters.AdminAuthorize]
        public IHttpActionResult GetArticles(string q = "", int pageIndex = JIFConstants.SYS_PAGE_INDEX, int pageSize = JIFConstants.SYS_PAGE_SIZE)
        {
            return JsonOk(_articleService.GetArticles(q, false, pageIndex, pageSize).ToPagedData());
        }

        [HttpGet]
        public IHttpActionResult GetCategories()
        {
            return JsonOk(_articleService.GetCategories());
        }

        [HttpGet]
        public IHttpActionResult GetArticle(int id)
        {
            return JsonOk(_articleService.GetArticle(id));
        }

        [HttpPost]
        public IHttpActionResult AddArticle(InsertArticleInput model)
        {
            _articleService.Insert(model);
            return JsonOk("文章添加成功");
        }

        [HttpPost]
        public IHttpActionResult UpdateArticle(int id, InsertArticleInput model)
        {
            _articleService.Update(id, model);
            return JsonOk("文章修改成功");
        }

        [HttpPost]
        public IHttpActionResult AddCategory(ArticleCategory model)
        {
            return JsonFail("文章分类添加 - 未实现");
        }
    }
}
