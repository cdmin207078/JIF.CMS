using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Helpers
{
    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public static class RegexHelper
    {
        /// <summary>
        /// 手机号码
        /// 移动: 134(0-8), 135, 136, 137, 138, 139, 147, 150, 151, 152, 157, 158, 159, 178, 182, 183, 184, 187, 188, 198</p>
        /// 联通: 130, 131, 132, 145, 155, 156, 166, 171, 175, 176, 185, 186</p>
        /// 电信: 133, 153, 173, 177, 180, 181, 189, 199</p>
        /// global star: 1349</p>
        /// virtual operator: 170</p>
        /// </summary>
        private static readonly string REGEX_MOBILE = @"^((13[0-9])|(14[5,7,9])|(15[0-3,5-9])|(16[6])|(17[0,1,3,5-8])|(18[0-9])|(19[8,9]))\d{8}$";

        /// <summary>
        /// 固定电话
        /// </summary>
        private static readonly string REGEX_TEL = @"^0\d{2,3}[- ]?\d{7,8}";

        /// <summary>
        /// 15位 身份证号码
        /// </summary>
        private static readonly string REGEX_ID_CARD15 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";

        /// <summary>
        /// 18位 身份证号码
        /// </summary>
        private static readonly string REGEX_ID_CARD18 = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9Xx])$";

        /// <summary>
        /// 电子邮箱
        /// </summary>
        private static readonly string REGEX_EMAIL = @"^[A-Za-z\d]+([-_.][A-Za-z\d]+)*@([A-Za-z\d]+[-.])+[A-Za-z\d]{2,4}$";

        /// <summary>
        /// URL 地址
        /// </summary>
        private static readonly string REGEX_URL = @"[a-zA-z]+://[^\s]*";

        /// <summary>
        /// 中文
        /// </summary>
        private static readonly string REGEX_ZH = "^[\u4e00-\u9fa5]+$";

        /// <summary>
        /// IPV4
        /// <see cref="https://blog.csdn.net/mhmyqn/article/details/7909041"/>
        /// <![CDATA[]>
        /// </summary>
        private static readonly string REGEX_IPV4 = @"^((2[0-4]\d|25[0-5]|[1-9]?\d|1\d{2})\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

        /// <summary>
        /// IPV6
        /// </summary>
        private static readonly string REGEX_IPV6 = @"^([\da-fA-F]{1,4}:){7}([\da-fA-F]{1,4})$";

        private static bool Match(string input, string regex)
        {
            return !String.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, regex);
        }


        /// <summary>
        /// 校验固定电话号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsTel(string input) => Match(input, REGEX_TEL);

        /// <summary>
        /// 校验手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobile(string input) => Match(input, REGEX_MOBILE);

        /// <summary>
        /// 校验URL
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsURL(string input) => Match(input, REGEX_URL);

        /// <summary>
        /// 校验电子邮箱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input) => Match(input, REGEX_EMAIL);

        /// <summary>
        /// 校验IPV4 地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPV4(string input) => Match(input, REGEX_IPV4);

        /// <summary>
        /// 校验IPV6 地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPV6(string input) => Match(input, REGEX_IPV6);

        /// <summary>
        /// 校验十五位身份证号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIDCard15(string input) => Match(input, REGEX_ID_CARD15);

        /// <summary>
        /// 校验十八位身份证号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIDCard18(string input) => Match(input, REGEX_ID_CARD18);

        /// <summary>
        /// 校验字符串是否全为中文字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChinese(String input) => Match(input, REGEX_ZH);

    }
}
