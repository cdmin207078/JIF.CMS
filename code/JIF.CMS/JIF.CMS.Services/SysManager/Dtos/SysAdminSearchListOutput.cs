using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.SysManager.Dtos
{
    public class SysAdminSearchListOutput
    {
        /// <summary>
        /// 主键自增
        /// </summary>
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

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        public string CreateUserName { get; set; }
    }
}
