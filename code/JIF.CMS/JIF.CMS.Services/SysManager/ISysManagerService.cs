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

        IPagedList<SysAdmin> Load(Expression<Func<SysAdmin, bool>> whereLambda = null, int pageIndex = 1, int pageSize = int.MaxValue);
    }
}
