using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services.SysManager.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.SysManager
{
    public interface ISysManagerService : IBaseService
    {
        void Add(SysAdmin model);

        void Update(int id, SysAdminUpdateBasicInfo model);

        void Delete(int id);

        /// <summary>
        /// 用户列表搜索
        /// </summary>
        /// <param name="s">搜索关键字 {账号 / Email / 电话}</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        IPagedList<SysAdmin> Load(string s, int pageIndex = 1, int pageSize = int.MaxValue);
    }
}
