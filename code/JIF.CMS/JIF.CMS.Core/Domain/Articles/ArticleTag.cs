using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain.Articles
{
    public class ArticleTag : BaseEntity
    {
        public int TagId { get; set; }

        public int ArticleId { get; set; }
    }
}
