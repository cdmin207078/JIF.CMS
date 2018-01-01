using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Cache
{
    /// <summary>
    /// 缓存调用接口
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 获取指定 key 缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data">数据</param>
        /// <param name="cacheTime">过期时间(时间戳)</param>
        void Set<T>(string key, T data, int? cacheTimeStamp = null);

        /// <summary>
        /// 判断 Key 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsSet(string key);

        /// <summary>
        /// 移除缓存数据
        /// </summary>
        /// <param name="key">Key of cached item</param>
        void Remove(string key);

        /// <summary>
        /// 移除缓存数据, 根据正则表达式筛选条件移除
        /// </summary>
        /// <param name="pattern">String key pattern</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// Clear all cache data
        /// </summary>
        void Clear();
    }
}
