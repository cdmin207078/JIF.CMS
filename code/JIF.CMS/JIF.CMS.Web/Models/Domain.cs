using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.CMS.Web.Models
{
    /// <summary>
    /// 文章信息
    /// </summary>
    public class Article
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
    }
}