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
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Linq;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Management.API.Controllers
{
    public class WelcomeController : BaseController
    {
        [HttpPost]
        [ValidateReqestParams]
        public IHttpActionResult Login(LoginViewModel model)
        {
            if (model == null)
                throw new JIFException("登陆信息为空");

            var sysManagerService = Resolve<ISysManagerService>();
            var authenticationService = Resolve<IAuthenticationService>();

            var userInfo = sysManagerService.Login(model.Account, model.Password);

            if (userInfo != null)
            {
                var sysAdmin = sysManagerService.Get(userInfo.UserId);

                authenticationService.LoginIn(model.Account, model.Password);
                //authenticationService.SignIn(sysAdmin);

                return JsonOk("登陆成功");
            }

            return JsonFail("登陆失败");
        }

        [HttpGet]
        public async Task<IHttpActionResult> Now()
        {
            await Task.Run(() =>
            {
                Logger.Info(DateTime.Now);
            });

            return JsonOk(DateTime.Now.ToString());
        }

        #region RestSharpTest Methods

        [HttpGet]
        public async Task<IHttpActionResult> SayHello(string name)
        {
            await Task.Run(() =>
            {
                var a = Thread.CurrentThread.ManagedThreadId;

                //Thread.Sleep(RandomHelper.Gen(100, 500));
                Task.Delay(RandomHelper.GenNumber(1000, 2000));

                var b = Thread.CurrentThread.ManagedThreadId;

                Logger.Info(string.Format("{0},{1}-{2}", name, a, b));
            });
            return JsonOk(string.Format("Hello {0}", name));
        }

        [HttpPost]
        [ValidateReqestParams]
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

            var authenticationService = Resolve<IAuthenticationService>();

            authenticationService.LoginIn(user.Account, user.Password);
            //authenticationService.SignIn(user);

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

        #region Async Methods Test

        [HttpGet]
        public IHttpActionResult GetVersion(int ver)
        {
            Thread.Sleep(2000);
            var result = string.Format("ver:{0}", ver);
            Logger.Info("getVersion return : " + result + ", TID - " + Thread.CurrentThread.ManagedThreadId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetVersions()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            // 同步获取version - 20s
            //var versions = getVersions();

            // 异步 await 获取 - 4s
            var versions = await getVersionsAsync();

            watch.Stop();

            return JsonOk(string.Format("耗时: {0}s, 所有版本: {1}", watch.ElapsedMilliseconds / 1000, JsonConvert.SerializeObject(versions)));
        }

        private List<string> getVersions()
        {
            HttpClient httpClient = new HttpClient();

            List<string> version = new List<string>();

            // 同步获取version
            for (int i = 1; i <= 10; i++)
            {
                var ver = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=" + i);

                version.Add(ver.Result);
            }

            return version;
        }

        private async Task<List<string>> getVersionsAsync()
        {
            HttpClient httpClient = new HttpClient();


            //var ver1 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=1");
            //Logger.Info("Start ver1");

            //var ver2 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=2");
            //Logger.Info("Start ver2");

            //var ver3 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=3");
            //Logger.Info("Start ver3");

            //var ver4 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=4");
            //var ver5 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=5");
            //var ver6 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=6");
            //var ver7 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=7");
            //var ver8 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=8");

            //var ver9 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=9");
            //Logger.Info("Start ver9");

            //var ver10 = httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=10");


            // 逐个获取结果

            //version.Add(await ver1);
            //version.Add(await ver2);
            //version.Add(await ver3);
            //version.Add(await ver4);
            //version.Add(await ver5);
            //version.Add(await ver6);
            //version.Add(await ver7);
            //version.Add(await ver8);
            //version.Add(await ver9);
            //version.Add(await ver10);

            // 手动判断异步状态
            //while (true)
            //{
            //    if (ver1.IsCompleted
            //    && ver2.IsCompleted
            //    && ver3.IsCompleted
            //    && ver4.IsCompleted
            //    && ver5.IsCompleted
            //    && ver6.IsCompleted
            //    && ver7.IsCompleted
            //    && ver8.IsCompleted
            //    && ver9.IsCompleted
            //    && ver10.IsCompleted)
            //    {
            //        version.Add(ver1.Result);
            //        version.Add(ver2.Result);
            //        version.Add(ver3.Result);
            //        version.Add(ver4.Result);
            //        version.Add(ver5.Result);
            //        version.Add(ver6.Result);
            //        version.Add(ver7.Result);
            //        version.Add(ver8.Result);
            //        version.Add(ver9.Result);
            //        version.Add(ver10.Result);

            //        break;
            //    }
            //}


            // whenAll

            var tasks = new List<Task<string>>();

            for (int i = 1; i <= 10; i++)
            {
                tasks.Add(httpClient.GetStringAsync("http://localhost:60002/welcome/getversion?ver=" + i));
            }

            return (await Task.WhenAll(tasks)).ToList();
        }



        [HttpGet]
        public async Task<IHttpActionResult> PayOrder()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            Logger.Info("处理逻辑开始 - TID - " + Thread.CurrentThread.ManagedThreadId);

            // 第一步, 获取订单
            var user = getUserInfo();

            // 第二步, 获取用户信息
            var order = getOrderInfo();

            // 第三部, 处理订单
            await DealOrder(await user, await order);

            // 第四部, 记录日志
            OrderLogging();

            watch.Stop();

            return JsonOk(string.Format("耗时: {0}s, user: {1}, order: {2}", watch.ElapsedMilliseconds / 1000, await user, await order));
        }


        private async Task<string> getUserInfo()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Logger.Info("getUserInfo - TID - " + Thread.CurrentThread.ManagedThreadId);
                return "久石让";
            });
        }

        private async Task<string> getOrderInfo()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(3000);
                Logger.Info("getOrderInfo - TID - " + Thread.CurrentThread.ManagedThreadId);

                return "JW201711070001";
            });
        }

        private async Task DealOrder(string name, string orderno)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(4000);
                Logger.Info(string.Format("DealOrder - TID - {0}, user: {1}, order: {2}", Thread.CurrentThread.ManagedThreadId, name, orderno));
            });
        }

        private void OrderLogging()
        {
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Logger.Info("OrderLogging - TID - " + Thread.CurrentThread.ManagedThreadId);
            });
        }

        #endregion

        #region CsvHelper Test

        [AcceptVerbs("Get", "POST")]
        public IHttpActionResult ShowInfo(dynamic data)
        {
            return JsonOk(new { name = data.name, age = data.age, gender = data.gender });
        }

        #endregion
    }
}
