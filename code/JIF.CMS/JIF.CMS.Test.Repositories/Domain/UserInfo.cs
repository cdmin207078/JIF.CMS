using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Test.Repositories.Domain
{
    [Table("user_info")]
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 座右铭
        /// </summary>
        public string Motto { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public enum GenderEnum
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// 男性
            /// </summary>
            Male = 1,

            /// <summary>
            /// 女性
            /// </summary>
            Female = 2
        }
    }
}
