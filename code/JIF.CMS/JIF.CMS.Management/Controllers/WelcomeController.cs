using JIF.CMS.Core;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using JIF.CMS.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace JIF.CMS.Management.Controllers
{
    public class WelcomeController : BaseController
    {
        private readonly ISysManagerService _sysManagerService;
        private readonly IAuthenticationService _authenticationService;

        public WelcomeController(ISysManagerService sysManagerService,
            IAuthenticationService authenticationService)
        {
            _sysManagerService = sysManagerService;
            _authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string account, string password, string returnUrl)
        {
            AntiForgery.Validate();

            var userInfo = _sysManagerService.Login(account, password);

            if (userInfo != null)
            {
                var sysAdmin = _sysManagerService.Get(userInfo.UserId);

                _authenticationService.SignIn(sysAdmin, true);

                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/");
            }

            return View();
        }
    }
}