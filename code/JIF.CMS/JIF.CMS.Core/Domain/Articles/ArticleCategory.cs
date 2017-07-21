﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain.Articles
{
    public partial class ArticleCategory : TreeRelationObject
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        ///// <summary>
        ///// 分类排序
        ///// </summary>
        //public int OrderIndex { get; set; }

        ///// <summary>
        ///// 所属父级分类编号
        ///// </summary>
        //public int ParentId { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImg { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
