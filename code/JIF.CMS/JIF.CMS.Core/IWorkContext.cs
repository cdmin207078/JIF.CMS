


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
        IUser CurrentUser { get; }

        /// <summary>
        /// 是否是系统管理员
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
