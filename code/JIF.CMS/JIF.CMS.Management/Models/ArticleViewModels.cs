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
    }
}