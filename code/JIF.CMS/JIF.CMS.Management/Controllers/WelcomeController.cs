using JIF.CMS.Core;
using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Helpers;
using JIF.CMS.Services;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using JIF.CMS.Web.Framework.Controllers;
using Newtonsoft.Json;
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
        private readonly ICacheManager _cacheManager;

        public WelcomeController(ISysManagerService sysManagerService,
            IAuthenticationService authenticationService,
            ICacheManager cacheManager)
        {
            _sysManagerService = sysManagerService;
            _authenticationService = authenticationService;
            _cacheManager = cacheManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetVerifyCode()
        {
            var verifyCode = RandomHelper.GenString(RandomHelper.CharSchemeEnum.NumChar, 4);

            var algo = EncryptHelper.CreateHashAlgoMd5();
            var codeKey = EncryptHelper.Encrypt(algo, Guid.NewGuid().ToString());

            _cacheManager.Set(string.Format(CacheKeyConstants.LOGIN_VERIFY_CODE, codeKey), verifyCode, TimeSpan.FromMinutes(1));

            var cookie = new HttpCookie("login-verify-code", codeKey);
            Response.SetCookie(cookie);

            // 图片
            var img = ImageHelper.GenValidateCode(verifyCode, 87, 34);

            return File(img, "image/jpeg");
        }

        [HttpPost]
        public ActionResult Index(string account, string password, string verifyCode, string returnUrl)
        {
            AntiForgery.Validate();

            var codeKey = Request.Cookies["login-verify-code"].Value;
            var cacheCode = _cacheManager.Get<string>(string.Format(CacheKeyConstants.LOGIN_VERIFY_CODE, codeKey));
            if (!string.Equals(verifyCode, cacheCode, StringComparison.OrdinalIgnoreCase))
            {
                throw new JIFException("验证码有误");
            }

            var userInfo = _sysManagerService.Login(account, password);

            if (userInfo != null)
            {
                var sysAdmin = _sysManagerService.Get(userInfo.UserId);

                _authenticationService.SignIn(account, password);

                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/");
            }

            return View();
        }
    }
}