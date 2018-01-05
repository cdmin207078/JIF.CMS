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
            // 验证码
            var algo = EncryptHelper.CreateHashAlgoMd5();
            var verifyCode = RandomHelper.GenString(RandomHelper.CharSchemeEnum.NumChar, 4);
            var vcc = EncryptHelper.Encrypt(algo, JIFConstants.MD5_Salt + verifyCode);

            _cacheManager.Set("verify-code-" + vcc, string.Empty);

            var cookie = new HttpCookie("verifyCode", vcc);

            Response.SetCookie(cookie);

            return View();
        }

        [HttpGet]
        public ActionResult GetVerifyCode()
        {
            // 验证码
            var algo = EncryptHelper.CreateHashAlgoMd5();
            var verifyCode = RandomHelper.GenString(RandomHelper.CharSchemeEnum.NumChar, 4);
            var vcc = EncryptHelper.Encrypt(algo, JIFConstants.MD5_Salt + verifyCode);

            _cacheManager.Set("verify-code-" + verifyCode, string.Empty);

            var cookie = new HttpCookie("verifyCode", vcc);
            Response.SetCookie(cookie);

            // 图片
            var img = ImageHelper.GenValidateCode(verifyCode, 87, 34);

            return File(img, "image/jpeg");
        }

        [HttpPost]
        public ActionResult Index(string account, string password, string verifyCode, string returnUrl)
        {
            AntiForgery.Validate();

            if (!_cacheManager.IsSet("verify-code-" + verifyCode))
            {
                throw new JIFException("验证码有误");
            }

            var userInfo = _sysManagerService.Login(account, password);

            if (userInfo != null)
            {
                var sysAdmin = _sysManagerService.Get(userInfo.UserId);

                _authenticationService.SignIn(sysAdmin);

                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return Redirect("/");
            }

            return View();
        }
    }
}