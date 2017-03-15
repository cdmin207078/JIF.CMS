using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services
{
    public partial interface IBaseService
    {
        /// <summary>
        /// 根据主键编号获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(object id) where T : BaseEntity;

        /// <summary>
        /// 条件搜索, 返回分页结果
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IPagedList<T> Search<T>(Expression<Func<T, bool>> whereLambda = null, int pageIndex = 0, int pageSize = int.MaxValue) where T : BaseEntity;
    }
}
