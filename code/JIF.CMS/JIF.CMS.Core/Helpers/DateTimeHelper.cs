using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Helpers
{
    // http://www.cnblogs.com/delphinet/archive/2011/06/09/2075985.html - DataTime.Now.Ticks精确的时间单位[转]
    public static class DateTimeHelper
    {
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式, 当前时间距离 1970-01-01 00:00:00:000 的 秒数
        /// </summary>
        /// <param name="time">系统当前时区,日期时间</param>
        /// <returns>long</returns>
        public static long ConvertToTimestamp(DateTime time)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((time.ToUniversalTime() - start).TotalSeconds);
        }

        /// <summary>
        /// 讲Unix时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timestamp”>Unix 标准时间戳</param>
        /// <returns>返回系统当前时区时间</returns>
        public static DateTime ConvertToDateTime(long timestamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = new TimeSpan(timestamp * Convert.ToInt32(Math.Pow(10, 7))); // Ticks 100 毫微秒 => 10^-7 秒

            return start.Add(ts).ToLocalTime();
        }
    }
}
