using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain
{
    public enum ArticleStatus : byte
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Darft = 0,

        /// <summary>
        /// 已发布
        /// </summary>
        Pub = 1,

        /// <summary>
        /// 已删除
        /// </summary>
        Del = 2
    }
}
