using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain.Articles
{
    public class Article : BaseEntity
    {
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 正文内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Markdown 格式内容
        /// </summary>
        public string MarkdownContent { get; set; }

        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// 已发布
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 所属分类编号
        /// </summary>
        public int CategoryId { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? UpdateUserId { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishTime { get; set; }
    }
}
