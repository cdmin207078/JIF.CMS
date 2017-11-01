using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core;

namespace JIF.CMS.Services.Authentication
{
    public class WebApiFormsCookiesAuthenticationService : IAuthenticationService
    {
        public IUser GetAuthenticatedUser()
        {
            throw new NotImplementedException();
        }

        public void SignIn(IUser user)
        {
            throw new JIFException("生成token, 关联redis, 返回用户信息. 未实现");
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
