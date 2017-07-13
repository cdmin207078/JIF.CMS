using JIF.CMS.Core;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Management.Models;
using JIF.CMS.Services.Articles;
using JIF.CMS.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace JIF.CMS.Management.Controllers
{
    public class ArticleController : AdminControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: Article
        public ActionResult Index(string Q = "", int pageIndex = JIFConsts.SYS_PAGE_INDEX, int pageSize = JIFConsts.SYS_PAGE_SIZE)
        {
            Q = Q.Trim();

            ViewBag.list = _articleService.GetArticles(Q, pageIndex, pageSize);

            ViewBag.Q = Q;

            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            ArticleEditViewModel vm = new ArticleEditViewModel
            {
                Article = new Article(),
                Categories = _articleService.GetCategories()
            };

            ViewBag.Title = "撰写文章";

            return View("Detail", vm);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var article = _articleService.GetArticle(id);
            if (article == null)
            {
                return RedirectToAction("Index");
            }

            ArticleEditViewModel vm = new ArticleEditViewModel
            {
                Article = article,
                Categories = _articleService.GetCategories()
            };

            ViewBag.Title = string.Format("编辑 - " + article.Title);

            return View(vm);
        }

        [HttpPost]
        // 保存文章
        public JsonResult Save()
        {
            return AjaxOk();
        }

        public ContentResult A()
        {
            return Content("A - 无延时");
        }

        public ContentResult B(string message)
        {
            Thread.Sleep(1000);
            return Content("[+1s] " + message);
        }

        public ContentResult C(string message)
        {
            Thread.Sleep(2000);
            //throw new NotImplementedException();
            return Content("[+2s]" + message);
        }
    }
}