using Common.Logging;
using JIF.CMS.Management.API.Models;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using JIF.CMS.WebApi.Framework.Controllers;
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


        [HttpPost]
        public IHttpActionResult Hello(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return JsonFail("no name");

            return JsonOk(string.Format("Hello, {0}", name));
        }

        [HttpGet]
        public IHttpActionResult Now()
        {
            var log = LogManager.GetLogger<WelcomeController>();

            //log.Trace("trace");
            //log.Debug("debug");
            //log.Info("info");
            //log.Warn("warn");
            //log.Error("error");
            //log.Fatal("fatal");

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
                },

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
                },

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
                },

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
                },

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
                },

            };

            log.Info(string.Format("传入数据: {0}", JsonConvert.SerializeObject(models)));

            return JsonOk(DateTime.Now.ToString());
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
