using JIF.CMS.Core;

namespace JIF.CMS.Services.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService
    {
        /// <summary>
        /// 获取已通过授权登陆的用户信息
        /// </summary>
        AuthenticatedUser GetAuthenticatedUser(string sessionID);

        /// <summary>
        /// 用户登录系统, 返回 sessionID
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns>sessionID</returns>
        string LoginIn(string account, string password);

        /// <summary>
        /// 用户登出系统
        /// </summary>
        void LoginOut(string sessionID);
    }
}
