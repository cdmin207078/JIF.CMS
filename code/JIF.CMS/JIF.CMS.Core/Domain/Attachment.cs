using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain
{
    public class Attachment : BaseEntity
    {

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public double Size { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }
    }
}
