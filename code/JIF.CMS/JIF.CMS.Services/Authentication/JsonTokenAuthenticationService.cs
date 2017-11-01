using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core.Domain;

namespace JIF.CMS.Services.Authentication
{
    public class JsonTokenAuthenticationService : IAuthenticationService
    {
        public IUser GetAuthenticatedUser()
        {
            throw new NotImplementedException("JsonTokenAuthentication.GetAuthenticatedUser 未实现");
        }

        public void SignIn(IUser user, bool createPersistentCookie)
        {
            throw new NotImplementedException("JsonTokenAuthentication.Sign 未实现");
        }

        public void SignOut()
        {
            throw new NotImplementedException("JsonTokenAuthentication.SignOut 未实现");
        }
    }
}
