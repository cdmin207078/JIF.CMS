using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Cache
{
    /*
     * 参考：
     * http://www.cnblogs.com/TianFang/p/3430169.html - 使用.net的Cache框架快速实现Cache操作
     * https://msdn.microsoft.com/zh-cn/library/system.runtime.caching.memorycache(v=vs.110).aspx - MemoryCache 类
     */

    public class MemoryCacheManager : ICacheManager
    {
        public void Clear()
        {
            foreach (var item in MemoryCache.Default)
            {
                this.Remove(item.Key);
            }
        }

        public T Get<T>(string key)
        {
            return (T)MemoryCache.Default.Get(key);
        }

        public bool IsSet(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T data, TimeSpan? cacheTime = null)
        {
            // 缓存处理策略
            //var policy = new CacheItemPolicy
            //{
            //    // 滑动过期 - 按访问频度决定超期
            //    // 它表示当对象在规定时间内没有得到访问时, 就会过期. 相对的, 如果对象一直被访问, 则不会过期。
            //    // 与 AbsoluteExpiration - 绝对时间过期 不能同时使用
            //    //SlidingExpiration = TimeSpan.FromMinutes(30),

            //    // 绝对过期 - 表示的是一个绝对时间过期, 当超过规定时间后, Cache内容就会过期。
            //    //AbsoluteExpiration = TimeSpan.FromDays(1),

            //    // 缓存被 移除 & 改变 之后的触发回调, 方便我们记日志或执行一些处理操作
            //    //RemovedCallback = new CacheEntryRemovedCallback(null),
            //    //UpdateCallback = new CacheEntryUpdateCallback(null);

            //    // 缓存依赖 - CacheEntryChangeMonitor、FileChangeMonitor、HostFileChangeMonitor、SqlChangeMonitor
            //    //ChangeMonitors = null,

            //    //  在服务器释放系统内存时，具有该优先级级别的缓存项将不会被自动从缓存删除。
            //    //  但是，具有该优先级级别的项会根据项的绝对到期时间或可调整到期时间与其他项一起被移除。
            //    // Priority = CacheItemPriority.Default
            //    //Priority = CacheItemPriority.NotRemovable

            //    // 指定的优先级别设置，用于决定是否逐出缓存项。
            //    // Default - 指示没有删除缓存项的优先级.
            //    // NotRemovable - 指示从缓存中某个缓存项应永远不会被删除。
            //    //Priority = CacheItemPriority.Default

            //};

            var policy = new CacheItemPolicy();

            if (cacheTime.HasValue)
                policy.SlidingExpiration = cacheTime.Value;

            MemoryCache.Default.Add(key, data, policy);
        }
    }
}
