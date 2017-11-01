using Common.Logging;
using JIF.CMS.Management.API.Models;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using JIF.CMS.WebApi.Framework.Controllers;
using JIF.CMS.WebApi.Framework.Filters;
using Newtonsoft.Json;
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

                return JsonOk("登陆成功");
            }

            return JsonFail("登陆失败");
        }

        [HttpGet]
        public IHttpActionResult Now()
        {
            Logger.Info("进入 Welcome.Now 方法");

            var models = new List<LoginViewModel>
            {
                new LoginViewModel
                {
                    Account = "admin",
                    Password = "admin1234567890!123",
                    Captcha = "PXQ4"
                },
                new LoginViewModel
                {
                    Account = "小风",
                    Password = "xiaofeng!@@",
                    Captcha = "QOCS"
                },
                new LoginViewModel
                {
                    Account = "暗雲",
                    Password = "ANYUN",
                    Captcha = "12ac"
                }
            };

            Logger.Info(string.Format("传入数据: {0}", JsonConvert.SerializeObject(models)));

            //var a = 1;
            //var b = 0;
            //var c = a / b;  // throw exception


            return JsonOk(DateTime.Now.ToString());
        }
    }
}
