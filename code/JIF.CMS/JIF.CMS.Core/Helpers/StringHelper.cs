using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// 将字符串转化为大驼峰形式
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="splitChar">单词分隔字符</param>
        /// <returns></returns>
        public static string GetFirstUppercase(string str, char[] splitChar)
        {
            var strArr = str.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < strArr.Length; i++)
            {
                sb.Append(strArr[i].Substring(0, 1).ToUpper() + strArr[i].Substring(1));
            }

            return sb.ToString();
        }
    }
}