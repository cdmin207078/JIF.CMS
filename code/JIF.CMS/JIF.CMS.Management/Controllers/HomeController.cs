using JIF.CMS.Core;
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
        public readonly IWorkContext _workContext;
        private readonly IAuthenticationService _authenticationService;

        public HomeController(IWorkContext workContext, IAuthenticationService authenticationService)
        {
            _workContext = workContext;
            _authenticationService = authenticationService;
        }


        private JIFConfig GetConfig()
        {
            //var config = HttpRuntime.Cache.Get("JIFConfig") as JIFConfig;
            //if (config != null)
            //    return config;

            //var appsettings = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            //var cdp = new CacheDependency(appsettings);
            //var setting = JsonConvert.DeserializeObject<JIFConfig>(System.IO.File.ReadAllText(appsettings));

            //HttpContext.Cache.Insert("JIFConfig", setting, cdp);

            //return setting;

            ObjectCache cache = MemoryCache.Default;
            var config = cache["JIFConfig"] as JIFConfig;

            if (config == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                var appSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new[] { appSettingsFile }));

                config = JsonConvert.DeserializeObject<JIFConfig>(System.IO.File.ReadAllText(appSettingsFile));

                cache.Add("JIFConfig", config, policy);
            }
            return config;
        }

        public ActionResult Index()
        {
            return View(GetConfig());
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