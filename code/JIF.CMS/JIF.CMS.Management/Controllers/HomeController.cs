using JIF.CMS.Core;
using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Configuration;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Web.Framework.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
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
            _authenticationService.SignOut();
            return RedirectToAction("index", "welcome");
        }
    }
}