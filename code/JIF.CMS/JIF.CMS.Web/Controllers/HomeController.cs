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
            return View();
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