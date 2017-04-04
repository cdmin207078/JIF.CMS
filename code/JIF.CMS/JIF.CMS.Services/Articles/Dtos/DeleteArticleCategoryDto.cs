using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles.Dtos
{
    public class DeleteArticleCategoryDto
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// 是否一并删除分类下所有文章
        /// </summary>
        public bool DelArticle { get; set; }
    }
}
