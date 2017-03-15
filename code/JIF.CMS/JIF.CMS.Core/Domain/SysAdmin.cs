using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain
{
    public class SysAdmin : BaseEntity
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者系统编号
        /// </summary>
        public int CreateUserId { get; set; }

    }
}
