using JIF.CMS.Core;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Services;
using JIF.CMS.Services.Authentication;
using System.Web;

namespace JIF.CMS.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        private AuthenticatedUser _cachedUser;

        public AuthenticatedUser CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                var cu = HttpContext.Current.Request.Cookies[JIFConstants.COOKIES_LOGIN_USER];

                if (cu != null && !string.IsNullOrWhiteSpace(cu.Value))
                {
                    _cachedUser = EngineContext.Current.Resolve<IAuthenticationService>().GetAuthenticatedUser(cu.Value);
                }

                return _cachedUser;
            }
        }
    }
}
