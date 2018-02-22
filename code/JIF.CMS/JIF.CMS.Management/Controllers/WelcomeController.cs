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
        private readonly IAuthenticationService _authenticationService;
        private readonly ICacheManager _cacheManager;

        public WelcomeController(IAuthenticationService authenticationService,
            ICacheManager cacheManager)
        {
            _authenticationService = authenticationService;
            _cacheManager = cacheManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        // 获取验证码
        [HttpGet]
        public ActionResult GetVerifyCode()
        {
            // 生成验证码
            var verifyCode = RandomHelper.GenString(RandomHelper.CharSchemeEnum.NumChar, 4);
            var verifyCodeImg = ImageHelper.GenValidateCode(verifyCode, 87, 34);

            var algo = EncryptHelper.CreateHashAlgoMd5();
            var codeKey = EncryptHelper.Encrypt(algo, Guid.NewGuid().ToString());

            // 验证码三分钟有效期
            _cacheManager.Set(string.Format(CacheKeyConstants.LOGIN_VERIFY_CODE, codeKey), verifyCode, TimeSpan.FromMinutes(3));

            var cookie = new HttpCookie("login-verify-code", codeKey);
            Response.SetCookie(cookie);

            return File(verifyCodeImg, "image/jpeg");
        }

        // 用户登陆处理
        [HttpPost]
        public ActionResult Index(string account, string password, string verifyCode, string returnUrl)
        {
            AntiForgery.Validate();

            // 校验验证码
            var codeKey = Request.Cookies["login-verify-code"].Value;
            var cacheCode = _cacheManager.Get<string>(string.Format(CacheKeyConstants.LOGIN_VERIFY_CODE, codeKey));
            if (!string.Equals(verifyCode, cacheCode, StringComparison.OrdinalIgnoreCase))
            {
                throw new JIFException("验证码有误");
            }

            // 用户登陆
            var sessionID = _authenticationService.LoginIn(account, password);

            // 记录 cookies
            var cookie = new HttpCookie(JIFConstants.COOKIES_LOGIN_USER, sessionID);
            cookie.Expires = DateTime.Now + JIFConstants.COOKIES_LOGIN_EXPIRE_TIME;
            Response.Cookies.Add(cookie);

            // 删除验证码cookies
            Response.Cookies.Remove("login-verify-code");

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return Redirect("/");
        }
    }
}