using JIF.CMS.Core.Domain;

namespace JIF.CMS.Core
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        AuthenticatedUser CurrentUser { get; }
    }


    /// <summary>
    /// 认证用户信息
    /// </summary>
    public class AuthenticatedUser
    {
        public int Id { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string CellPhone { get; set; }
    }
}