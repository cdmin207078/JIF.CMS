using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain
{
    /// <summary>
    /// 用户抽象
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        int Id { get; set; }


        /// <summary>
        /// 用户账号
        /// </summary>
        string Account { get; set; }
    }
}
