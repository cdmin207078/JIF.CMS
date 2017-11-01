using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core.Domain;
using System.Web;

namespace JIF.CMS.Services.Authentication
{
    public class JsonTokenAuthenticationService : IAuthenticationService
    {
        private IUser _cachedUser;

        private readonly HttpContextBase _httpContext;

        public JsonTokenAuthenticationService(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        private IUser GetAuthenticatedUserFromToken()
        {
            return null;
        }

        public IUser GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext == null ||
                _httpContext.Request == null)
            {
                return null;
            }

            _cachedUser = GetAuthenticatedUserFromToken();

            return _cachedUser;
        }

        public void SignIn(IUser user)
        {
            throw new NotImplementedException("JsonTokenAuthentication.Sign 未实现");
        }

        public void SignOut()
        {
            throw new NotImplementedException("JsonTokenAuthentication.SignOut 未实现");
        }
    }
}
