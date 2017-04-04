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
    public interface ISysManagerService
    {
        SysAdmin Get(int id);

        /// <summary>
        /// 新增 管理员信息
        /// </summary>
        /// <param name="model"></param>
        void Add(SysAdminInertBasicInfo model);

        /// <summary>
        /// 修改 管理员基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        void UpdateBasicInfo(int id, SysAdminUpdateBasicInfo model);

        /// <summary>
        /// 修改 管理员登录密码
        /// </summary>
        /// <param name="originalPwd"></param>
        /// <param name="newPwd"></param>
        void UpdatePwd(int id, string newPwd);

        /// <summary>
        /// 删除 管理员
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="s">搜索关键字 {账号 / Email / 电话}</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        IPagedList<SysAdminSearchListOutDto> Get(string q, int pageIndex = 1, int pageSize = int.MaxValue);

        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        LoginOutputDto Login(string account, string password);
    }
}
