using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.Attachments
{
    public interface IAttachmentService
    {
        void Insert(Attachment model);

        void Delete(int id);

        /// <summary>
        /// 附件列表
        /// </summary>
        /// <param name="s">搜索关键字 {文件名}</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        IPagedList<Attachment> GetArticles(string q, int pageIndex = 1, int pageSize = int.MaxValue);
    }
}
