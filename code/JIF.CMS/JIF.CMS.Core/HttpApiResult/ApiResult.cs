using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.HttpApiResult
{
    public class APIResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
    }

    public class APIResult<T> : APIResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
}
