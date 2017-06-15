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
        // GET: Article
        public ActionResult Index()
        {
            return View();
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