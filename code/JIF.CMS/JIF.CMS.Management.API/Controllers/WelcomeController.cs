using JIF.CMS.Management.API.Models;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using JIF.CMS.WebApi.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JIF.CMS.Management.API.Controllers
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

        [HttpPost]
        public IHttpActionResult Login(LoginViewModel model)
        {
            var userInfo = _sysManagerService.Login(model.Account, model.Password);

            if (userInfo != null)
            {
                var sysAdmin = _sysManagerService.Get(userInfo.UserId);

                _authenticationService.SignIn(sysAdmin, true);

                return AjaxOk("登陆成功");
            }

            return AjaxFail("登陆失败");
        }


        [HttpPost]
        public IHttpActionResult Hello(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return AjaxFail("no name");

            return AjaxOk(string.Format("Hello, {0}", name));
        }


        [HttpGet]
        public IHttpActionResult Now()
        {
            return Ok(DateTime.Now.ToString());
        }

        [HttpPost]
        public IHttpActionResult LoginTo(LoginViewModel model)
        {
            return Ok(string.Format("account: {0}, password: {1}, code: {2}", model.Account, model.Password, model.Captcha));
        }


        [HttpPost]
        public IHttpActionResult GetStringArray(List<string> names)
        {
            return Ok(string.Join(",", names));
        }
    }
}
