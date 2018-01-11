using JIF.CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService
    {
        /// <summary>
        /// 用户登录系统
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        void SignIn(string account, string password);

        /// <summary>
        /// 用户登出系统
        /// </summary>
        void SignOut();
    }
}
