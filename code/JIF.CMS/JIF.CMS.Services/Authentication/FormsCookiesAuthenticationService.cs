using JIF.CMS.Core.Domain;
using JIF.CMS.Services.SysManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace JIF.CMS.Services.Authentication
{
    public class FormsCookiesAuthenticationService : IAuthenticationService
    {
        private IUser _cachedUser;

        private readonly HttpContextBase _httpContext;
        private readonly TimeSpan _expirationTimeSpan;

        public ISysManagerService _sysManagerService { get; set; }

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

            var user = _sysManagerService.Get(int.Parse(uid));
            return user;
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
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="createPersistentCookie"></param>
        public void SignIn(IUser user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
              1 /*version*/,
              user.Account,
              now,
              now.Add(_expirationTimeSpan),
              createPersistentCookie,
              user.Id.ToString(),
              FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
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

        public void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }
    }
}
