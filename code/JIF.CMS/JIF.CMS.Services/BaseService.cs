using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Services
{
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// 根据主键ID, 获取实体信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get<T>(object id)
            where T : BaseEntity
        {
            return GetRepository<T>().Get(id);
        }

        /// <summary>
        /// 条件分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public virtual IPagedList<T> Search<T>(Expression<Func<T, bool>> whereLambda = null, int pageIndex = 1, int pageSize = int.MaxValue)
            where T : BaseEntity
        {
            var query = GetRepository<T>().Table;

            if (whereLambda != null)
                query = query.Where(whereLambda);

            return new PagedList<T>(query.OrderByDescending(d => d.Id), pageIndex, pageSize);
        }

        private IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return EngineContext.Current.Resolve<IRepository<T>>();
        }
    }
}
