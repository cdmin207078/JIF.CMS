using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIF.CMS.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        private readonly IAuthenticationService _authenticationService;

        private IUser _cachedUser;

        public WebWorkContext(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IUser CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                IUser user = null;

                // 针对后台job 调用,无 httpcontext 对象,则使用后台任务帐号
                // check whether request is made by a background task
                // in this case return built-in user record for background task

                // if (_httpContext == null)
                // {
                //     user = _userService.GetCustomerBySystemName(SystemCustomerNames.BackgroundTask);
                // }

                // registered user
                if (user == null)
                {
                    user = _authenticationService.GetAuthenticatedUser();
                }

                _cachedUser = user;

                return _cachedUser;
            }
        }

        public bool IsAdmin
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
