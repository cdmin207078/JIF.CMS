using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 重复字符串本身
        /// </summary>
        /// <param name="source"></param>
        /// <param name="count">重复次数</param>
        /// <returns>若count 小于 2 则返回字符串本身</returns>
        public static string Repeat(this string source, int count)
        {
            if (count < 2)
                return source;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append(source);
            }

            return sb.ToString();
        }
    }
}
