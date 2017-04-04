using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles.Dtos
{
    public class UpdateArticleInputDto : CreateArticleInputDto
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
