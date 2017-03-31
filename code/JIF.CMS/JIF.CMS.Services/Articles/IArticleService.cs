using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services.Articles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Articles
{
    public interface IArticleService
    {

        Article Get(int id);

        /// <summary>
        /// 新增 管理员信息
        /// </summary>
        /// <param name="model"></param>
        void Add(ArticleInertDto model);

        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="s">搜索关键字 {账号 / Email / 电话}</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        IPagedList<ArticleSearchListOutDto> Load(string q, int pageIndex = 1, int pageSize = int.MaxValue);
    }
}
