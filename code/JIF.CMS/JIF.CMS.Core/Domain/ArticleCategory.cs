using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain
{
    public partial class ArticleCategory : BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 所属父级分类编号
        /// </summary>
        public int ParentCategoryId { get; set; }
    }
}
