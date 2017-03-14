using JIF.CMS.Core.Domain;
using JIF.CMS.Services.SysManager.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services.SysManager
{
    public interface ISysManagerService
    {
        void Add(SysAdmin model);

        void Update(int id, SysAdminUpdateBasicInfo model);

        void Delete(int id);

    }
}
