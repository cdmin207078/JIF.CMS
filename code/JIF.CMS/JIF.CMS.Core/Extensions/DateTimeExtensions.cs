using JIF.CMS.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 时间转化为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime time)
        {
            return DateTimeHelper.ConvertToTimestamp(time);
        }
    }
}
