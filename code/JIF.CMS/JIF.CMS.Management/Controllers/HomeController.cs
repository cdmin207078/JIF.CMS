using JIF.CMS.Core;
using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Configuration;
using JIF.CMS.Services;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Web.Framework.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace JIF.CMS.Management.Controllers
{
    public class HomeController : AdminControllerBase
    {
        private readonly IWorkContext _workContext;
        private readonly JIFConfig _config;
        private readonly ICacheManager _cacheManager;
        private readonly IAuthenticationService _authenticationService;

        public HomeController(JIFConfig config, IWorkContext workContext, IAuthenticationService authenticationService, ICacheManager cacheManager)
        {
            _config = config;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            //ViewBag.RedisName = _cacheManager.Get<string>("name:string");
            ViewBag.RedisName = _cacheManager.Get<string>("cms:class:1:users");

            return View(_config);
        }

        public ActionResult CurrentUserInfo(string viewpath)
        {
            return View(viewpath, _workContext.CurrentUser);
        }

        public ActionResult LogOut()
        {
            var cu = Request.Cookies[JIFConstants.COOKIES_LOGIN_USER];

            if (cu != null && string.IsNullOrWhiteSpace(cu.Value))
            {
                _authenticationService.LoginOut(cu.Value);
                Request.Cookies.Remove(cu.Value);
            }

            return RedirectToAction("index", "welcome");
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