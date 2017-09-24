using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.CMS.Management.Models
{
    public class ArticleEditViewModel
    {
        public Article Article { get; set; }

        public List<TreeRelationObjectTraverseWrapper<ArticleCategory>> Categories { get; set; }

        public List<string> ArticleTags { get; set; }

        public List<string> Tags { get; set; }
    }


    public class ArticleCategoryInfoViewModel
    {
        public List<TreeRelationObjectTraverseWrapper<ArticleCategory>> Categories { get; set; }

        public ArticleCategory Category { get; set; }

        //public int Id { get; set; }

        ///// <summary>
        ///// 分类名称
        ///// </summary>
        //public string Name { get; set; }

        ///// <summary>
        ///// 封面图片
        ///// </summary>
        //public string CoverImg { get; set; }

        ///// <summary>
        ///// 描述
        ///// </summary>
        //public string Description { get; set; }
    }
}