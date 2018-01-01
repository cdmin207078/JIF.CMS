using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services
{
    public static class JIFConstants
    {
        #region JIF System Setting

        /// <summary>
        /// 系统默认用户ID
        /// </summary>
        public const int SYS_DEFAULTUID = 1;

        /// <summary>
        /// 默认页码, 第一页
        /// </summary>
        public const int SYS_PAGE_INDEX = 1;

        /// <summary>
        /// 默认页数据行, 20行
        /// </summary>
        public const int SYS_PAGE_SIZE = 20;

        /// <summary>
        /// 每页显示数据条数
        /// </summary>
        public enum SYS_PAGE_SIZE_TYPE
        {
            /// <summary>
            /// 10
            /// </summary>
            Ten = 10,

            /// <summary>
            /// 20
            /// </summary>
            Twenty = 20,

            /// <summary>
            /// 50
            /// </summary>
            Fifty = 50,

            /// <summary>
            /// 100
            /// </summary>
            Hundred = 100
        }

        #endregion

        #region Format DateTime

        /// <summary>
        /// 默认时间格式化
        /// </summary>
        public const string DATETIME_NORMAL = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 默认日期格式化
        /// </summary>
        public const string DATETIME_DATE = "yyyy-MM-dd";

        #endregion

        #region Security

        /// <summary>
        /// 系统MD5加密密盐
        /// </summary>
        public const string MD5_Salt = "AiAkEbwbNv8UP6BdcnqjF80CEzVfeLLN";

        #endregion
    }

    /// <summary>
    /// 缓存 key 定义类
    /// </summary>
    public static class CacheKeyConstants
    {
        /// <summary>
        /// 登陆用户基本信息
        /// </summary>
        public const string USER_INFO = "USER_INFO_{0}";

        /// <summary>
        /// 登陆用户权限列表
        /// </summary>
        public const string USER_PERMISSIONS = "USER_PERMISSIONS_{0}";
    }
}
