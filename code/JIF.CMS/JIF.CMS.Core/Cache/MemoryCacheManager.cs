using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Cache
{
    public class MemoryCacheManager : ICacheManager
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new JIFException("Get 未实现");
        }

        public bool IsSet(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T data, int? cacheTimeStamp = null)
        {
            throw new JIFException("Set 未实现");
        }
    }
}
