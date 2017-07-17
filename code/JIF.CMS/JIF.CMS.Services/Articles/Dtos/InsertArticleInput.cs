using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles.Dtos
{
    public class InsertArticleInput
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
        /// Markdown格式 正文内容
        /// </summary>
        public string MarkdownContent { get; set; }

        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// 是否已经发布
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// 所属分类编号
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
