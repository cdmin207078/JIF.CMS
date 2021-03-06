﻿using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Services.SysManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using JIF.CMS.Core;

namespace JIF.CMS.Services.Authentication
{
    public class FormsCookiesAuthenticationService : IAuthenticationService
    {
        private IUser _cachedUser;

        private readonly HttpContextBase _httpContext;
        private readonly TimeSpan _expirationTimeSpan;

        public FormsCookiesAuthenticationService(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        private IUser GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var uid = ticket.UserData;

            if (string.IsNullOrWhiteSpace(uid))
            {
                return null;
            }

            // TODO: 需要缓存一下. redis
            //var user = EngineContext.Current.Resolve<ISysManagerService>().Get(int.Parse(uid));

            //return user;
            throw new NotImplementedException("未实现方法");
        }


        /// <summary>
        /// 获取当前认证用户
        /// </summary>
        /// <returns></returns>
        public IUser GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);

            _cachedUser = user;

            return _cachedUser;
        }

        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="user"></param>
        public void SignIn(IUser user)
        {
            // 根据IsPersistent是否为true, 以及webconfig中的timeout来确定cookies的到期时间
            // 如果 Cookie 是持久的, 为 true；否则为 false
            var isPersistent = true;

            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
              1 /*version*/,
              user.Account,
              now,
              now.Add(_expirationTimeSpan),
              isPersistent,
              user.Id.ToString(),
              FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // 注意，在这里必须使用HttpOnly属性来防止Cookie被JavaScript读取，从而避免跨站脚本攻击（XSS攻击）
            // http://www.cnblogs.com/bangerlee/archive/2013/04/06/3002142.html - xss攻击入门
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedUser = user;
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        public void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public void SignIn(string account, string password)
        {
            throw new NotImplementedException();
        }

        public string LoginIn(string account, string password)
        {
            throw new NotImplementedException();
        }

        public void LoginOut(string sessionID)
        {
            throw new NotImplementedException();
        }

        AuthenticatedUser IAuthenticationService.GetAuthenticatedUser(string sessionID)
        {
            throw new NotImplementedException();
        }
    }
}
