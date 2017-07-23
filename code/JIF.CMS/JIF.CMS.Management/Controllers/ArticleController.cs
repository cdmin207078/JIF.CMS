using JIF.CMS.Core;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Management.Models;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Articles.Dtos;
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

        // 文章列表页面
        public ActionResult Index(string Q = "", int pageIndex = JIFConsts.SYS_PAGE_INDEX, int pageSize = JIFConsts.SYS_PAGE_SIZE)
        {
            Q = Q.Trim();

            ViewBag.list = _articleService.GetArticles(Q, pageIndex: pageIndex, pageSize: pageSize);

            ViewBag.Q = Q;

            var categories = _articleService.GetCategories();


            return View();
        }

        // 新增文章页面
        [HttpGet]
        public ActionResult Add()
        {
            ArticleEditViewModel vm = new ArticleEditViewModel
            {
                Article = new Article() { PublishTime = DateTime.Now },
                Categories = _articleService.GetCategories(),
                ArticleTags = new List<string>(),
                Tags = _articleService.GetTagsDict().Keys.ToList()
            };

            ViewBag.Title = "撰写文章";

            return View("Detail", vm);
        }

        // 文章详情页 & 指定文章编辑页面
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
                Categories = _articleService.GetCategories(),
                ArticleTags = _articleService.GetArticleTags(id).Select(d => d.Name).ToList(),
                Tags = _articleService.GetTagsDict().Keys.ToList()
            };

            ViewBag.Title = string.Format("编辑 - " + article.Title);

            return View(vm);
        }

        // 文章回收站页面
        [HttpGet]
        public ActionResult Recycled()
        {
            return View();
        }

        // 保存文章
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(int id, InsertArticleInput model)
        {
            if (id == 0)
            {
                _articleService.Insert(model);
            }
            else
            {
                _articleService.Update(id, model);
            }


            return AjaxOk();
        }

        // 删除文章
        [HttpPost]
        public JsonResult DeleteArticle(int id)
        {
            return AjaxOk();
        }

        // 分类列表
        [HttpGet]
        public ActionResult Categories()
        {
            return View(_articleService.GetCategoriesTreeRelation());
        }

        // 查看分类信息
        [HttpGet]
        public ActionResult CategoryInfo(int? id)
        {
            ViewBag.Categories = _articleService.GetCategoriesSortArray();
            return View();
        }

        // 保存分类
        [HttpPost]
        public JsonResult SaveCategories()
        {
            return AjaxOk();
        }

        // 删除分类
        [HttpPost]
        public JsonResult DeleteCategory()
        {
            return AjaxOk();
        }


        // 标签列表页面
        [HttpGet]
        public ActionResult Tags()
        {
            return View();
        }

        // 保存标签
        [HttpPost]
        public JsonResult SaveTag()
        {
            return AjaxOk();
        }

        // 删除标签
        [HttpPost]
        public JsonResult DelTag()
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