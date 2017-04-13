using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Management.API.Filters;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Articles.Dtos;
using System.Web.Http;
using System.Web.Http.Cors;

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
        public IHttpActionResult GetArticles(string q = "", int pageIndex = JIFConsts.SYS_PAGE_INDEX, int pageSize = JIFConsts.SYS_PAGE_SIZE)
        {
            return AjaxOk(_articleService.GetArticles(q, pageIndex, pageSize).ToPagedData());
        }

        [HttpGet]
        public IHttpActionResult GetCategories()
        {
            return AjaxOk(_articleService.GetCategories());
        }

        [HttpGet]
        public IHttpActionResult GetArticle(int id)
        {
            return AjaxOk(_articleService.GetArticle(id));
        }

        [HttpPost]
        public IHttpActionResult AddArticle(ArticleDto model)
        {
            _articleService.Insert(model);
            return AjaxOk("文章添加成功");
        }

        [HttpPost]
        public IHttpActionResult UpdateArticle(int id, ArticleDto model)
        {
            _articleService.Update(id, model);
            return AjaxOk("文章修改成功");
        }

        [HttpPost]
        public IHttpActionResult AddCategory(ArticleCategory model)
        {
            return AjaxFail("文章分类添加 - 未实现");
        }
    }
}
