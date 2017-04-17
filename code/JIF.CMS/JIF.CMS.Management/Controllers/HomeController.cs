using JIF.CMS.Core;
using JIF.CMS.Services.Articles;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult Index()
        {
            return View();
        }

        // GET: Layout
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