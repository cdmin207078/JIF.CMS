using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Web.Framework.Models
{
    /// <summary>
    /// 文件上传模式
    /// </summary>
    public enum Uploadmode
    {
        /// <summary>
        /// 新文件上传
        /// </summary>
        New,

        /// <summary>
        /// 断点续传
        /// </summary>
        Continued,

        /// <summary>
        /// 已经存在相同文件
        /// </summary>
        Existed
    }
}
