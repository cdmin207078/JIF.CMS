using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core.Domain;
using System.Web;
using Common.Logging;
using JIF.CMS.Core;
using JIF.CMS.Core.Cache;
using JIF.CMS.Core.Infrastructure;

namespace JIF.CMS.Services.Authentication
{
    public class JsonTokenAuthenticationService : IAuthenticationService
    {
        private readonly ILog Logger;
        private readonly ICacheManager _cache;

        private readonly HttpContextBase _httpContext;

        public JsonTokenAuthenticationService(HttpContextBase httpContext, ILog logger, ICacheManager cache)
        {
            _httpContext = httpContext;
            Logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// 通过token获取
        /// </summary>
        /// <returns></returns>
        private IUser GetAuthenticatedUserFromToken()
        {
            var token = _httpContext.Request.Headers.Get("token");
            var uid = _httpContext.Request.Headers.Get("uid");

            Logger.Info(string.Format("token: {0}, uid: {1}", token, uid));

            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(uid))
                return null;

            if (!_cache.IsSet(token))
                return null;

            return _cache.Get<IUser>(token);
        }

        /// <summary>
        /// 获取通过认证的用户信息
        /// </summary>
        /// <returns></returns>
        public IUser GetAuthenticatedUser()
        {
            if (_httpContext == null ||
                _httpContext.Request == null)
            {
                return null;
            }

            return GetAuthenticatedUserFromToken();
        }

        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="user"></param>
        public void LoginIn(IUser user)
        {
            var token = Guid.NewGuid().ToString("N");


            // 缓存用户信息
            _cache.Set(token, user);

            // 返回 token & uid
            _httpContext.Response.Headers.Add("token", token);
            _httpContext.Response.Headers.Add("uid", user.Id.ToString());


            //throw new NotImplementedException("JsonTokenAuthentication.Sign 未实现");
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        public void LoginOut(string sessionID)
        {
            throw new NotImplementedException("JsonTokenAuthentication.SignOut 未实现");
        }

        public string LoginIn(string account, string password)
        {
            throw new NotImplementedException();
        }

        AuthenticatedUser IAuthenticationService.GetAuthenticatedUser(string sessionID)
        {
            throw new NotImplementedException();
        }
    }
}
