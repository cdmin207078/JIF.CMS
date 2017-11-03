using Common.Logging;
using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Domain.Articles;
using JIF.CMS.Core.Infrastructure;
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
        [ValidateViewModel]
        public IHttpActionResult Login(LoginViewModel model)
        {
            if (model == null)
                throw new JIFException("登陆信息为空");

            var userInfo = _sysManagerService.Login(model.Account, model.Password);

            if (userInfo != null)
            {
                var sysAdmin = _sysManagerService.Get(userInfo.UserId);

                _authenticationService.SignIn(sysAdmin);

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


        #region RestSharpTest Methods

        [HttpGet]
        public IHttpActionResult SayHello(string name)
        {
            return JsonOk(string.Format("Hello {0}", name));
        }

        [HttpPost]
        [ValidateViewModel]
        public IHttpActionResult Register(LoginViewModel model)
        {
            if (model == null)
                throw new JIFException("注册信息为空");

            model.Account = string.Format("账号 - {0}", model.Account);
            model.Password = string.Format("密码 - {0}", model.Password);

            return JsonOk(model);
        }

        [HttpPost]
        public IHttpActionResult LoginIn()
        {
            var user = new SysAdmin
            {
                Id = 1,
                Account = "admin",
                CellPhone = "15618147550"
            };

            _authenticationService.SignIn(user);

            return JsonOk();
        }

        [AdminAuthorize]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetArticles()
        {
            var articles = new List<Article>()
            {
                new Article { Id = 1, Title = "Something Just Like This"  },
                new Article { Id = 2, Title = "My Songs Know What You Did In The Dark"  },
                new Article { Id = 3, Title = "Immortals (End Credit Version)"  },
            };

            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var user = workContext.CurrentUser;

            return JsonOk(new { posts = articles, user = user });
        }

        #endregion

    }
}
