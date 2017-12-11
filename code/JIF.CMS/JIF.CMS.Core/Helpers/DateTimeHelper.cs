using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式, 当前时间距离 1970-01-01 00:00:00:000 的 秒数
        /// </summary>
        /// <param name="time">日期时间</param>
        /// <returns>long</returns>
        public static long ConvertToTimestamp(DateTime time)
        {
            DateTime st = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();
            long t = (time.Ticks - st.Ticks) / 10000000;   //除10000调整为13位
            return t;
        }

        /// <summary>
        /// 讲Unix时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timestamp”>Unix 标准时间戳</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timestamp)
        {
            DateTime st = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();
            long lt = long.Parse(timestamp + "0000000");
            TimeSpan toNow = new TimeSpan(lt);
            return st.Add(toNow);
        }
    }
}
