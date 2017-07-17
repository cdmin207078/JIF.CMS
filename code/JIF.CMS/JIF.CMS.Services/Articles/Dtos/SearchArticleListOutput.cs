using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles.Dtos
{
    public class SearchArticleListOutput
    {
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 最后更新者
        /// </summary>
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 已发布
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishTime { get; set; }
    }
}
