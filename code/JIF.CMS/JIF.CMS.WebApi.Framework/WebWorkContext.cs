using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIF.CMS.WebApi.Framework
{
    public class WebWorkContext : IWorkContext
    {
        private IUser _cachedUser;

        public IUser CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                _cachedUser = EngineContext.Current.Resolve<IAuthenticationService>().GetAuthenticatedUser();

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
