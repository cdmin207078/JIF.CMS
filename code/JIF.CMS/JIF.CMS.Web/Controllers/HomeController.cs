using JIF.CMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            var vm = new HomePageViewModel
            {
                Articles = new List<Article>
                {
                     new Article { Title = "visual studio", Content = "Microsoft Visual Studio", CreateTime = DateTime.Now },
                     new Article { Title = "tencent", Content = "微信", CreateTime = DateTime.Now.AddMilliseconds(778930) }

                }
            };
            return View(vm);
        }

        // 归档
        public ViewResult Archives()
        {
            return View();
        }

        // 关于
        public ViewResult About()
        {
            return View();
        }
    }
}