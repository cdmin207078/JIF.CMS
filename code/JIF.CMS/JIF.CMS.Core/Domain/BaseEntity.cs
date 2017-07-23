using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain
{
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        public int Id { get; set; }
    }


    public abstract class InfoOperation : BaseEntity
    {

        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? UpdateUserId { get; set; }
    }
}
