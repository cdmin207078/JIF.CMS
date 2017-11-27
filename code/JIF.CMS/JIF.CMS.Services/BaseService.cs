using Common.Logging;
using JIF.CMS.Core;
using JIF.CMS.Core.Data;
using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Infrastructure;
using JIF.CMS.Services;
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
        protected readonly ILog Logger;

        public BaseService()
        {
            Logger = EngineContext.Current.Resolve<ILog>();
        }

        //private IRepository<T> _repository;

        //public BaseService(IRepository<T> repository)
        //{
        //    _repository = repository;
        //}

        //public IQueryable<T> Tables
        //{
        //    get
        //    {
        //        return _repository.Table;
        //    }
        //}

        //public T Get(object id)
        //{
        //    return _repository.Get(id);
        //}

        //public virtual IPagedList<T> Search(Expression<Func<T, bool>> whereLambda = null, int pageIndex = 1, int pageSize = int.MaxValue)
        //{
        //    var query = _repository.Table;

        //    if (whereLambda != null)
        //        query = query.Where(whereLambda);

        //    return new PagedList<T>(query.OrderByDescending(d => d.Id), pageIndex, pageSize);
        //}
    }
}
