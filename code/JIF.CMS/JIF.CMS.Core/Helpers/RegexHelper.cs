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
        /**
         * Regex of exact mobile.
         * <p>china mobile: 134(0-8), 135, 136, 137, 138, 139, 147, 150, 151, 152, 157, 158, 159, 178, 182, 183, 184, 187, 188, 198</p>
         * <p>china unicom: 130, 131, 132, 145, 155, 156, 166, 171, 175, 176, 185, 186</p>
         * <p>china telecom: 133, 153, 173, 177, 180, 181, 189, 199</p>
         * <p>global star: 1349</p>
         * <p>virtual operator: 170</p>
         */
        private static readonly string REGEX_MOBILE = "^((13[0-9])|(14[5,7,9])|(15[0-3,5-9])|(16[6])|(17[0,1,3,5-8])|(18[0-9])|(19[8,9]))\\d{8}$";
        /**
         * Regex of telephone number.
         */
        private static readonly string REGEX_TEL = "^0\\d{2,3}[- ]?\\d{7,8}";
        /**
         * Regex of id card number which length is 15.
         */
        private static readonly string REGEX_ID_CARD15 = "^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$";
        /**
         * Regex of id card number which length is 18.
         */
        private static readonly string REGEX_ID_CARD18 = "^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}([0-9Xx])$";
        /// <summary>
        /// <see cref="^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"/>
        /// <seealso cref="^(?!\\.)(\"([^ \"\r\\]|\\[\"\r\\])*\"|([-a-z0-9!#$%&'*+/=?^_`{|}~] |(?@[a-z0-9][\\w\\.-]*[a-z0-9]\\.[a-z][a-z\\.]*[a-z]$"/>
        /// </summary>
        private static readonly string REGEX_EMAIL = "^[A-Za-z\\d]+([-_.][A-Za-z\\d]+)*@([A-Za-z\\d]+[-.])+[A-Za-z\\d]{2,4}$";
        /**
         * Regex of url.
         */
        private static readonly string REGEX_URL = "[a-zA-z]+://[^\\s]*";
        /**
         * Regex of Chinese character.
         */
        private static readonly string REGEX_ZH = "^[\\u4e00-\\u9fa5]+$";
        /**
         * Regex of username.
         * <p>scope for "a-z", "A-Z", "0-9", "_", "Chinese character"</p>
         * <p>can't end with "_"</p>
         * <p>length is between 6 to 20</p>
         */
        private static readonly string REGEX_USERNAME = "^[\\w\\u4e00-\\u9fa5]{6,20}(?<!_)$";
        /**
         * Regex of date which pattern is "yyyy-MM-dd".
         */
        private static readonly string REGEX_DATE = "^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$";
        /**
         * Regex of ip address.
         */
        private static readonly string REGEX_IPV4 = "((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)";

        private static bool isMatch(string input, string regex)
        {
            return !String.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, regex);
        }

        /// <summary>
        /// 校验手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobile(string input) { return isMatch(input, REGEX_MOBILE); }

        /// <summary>
        /// 校验固定电话号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsTel(string input) { return isMatch(input, REGEX_TEL); }

        /// <summary>
        /// 校验电子邮箱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input) { return isMatch(input, REGEX_EMAIL); }

        /// <summary>
        /// 校验十五位身份证号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIDCard15(string input) { return isMatch(input, REGEX_ID_CARD15); }

        /// <summary>
        /// 校验十八位身份证号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIDCard18(string input) { return isMatch(input, REGEX_ID_CARD18); }

        /// <summary>
        /// 校验IP地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPV4(string input) { return isMatch(input, REGEX_IPV4); }

        /// <summary>
        /// 校验十八位身份证号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsURL(string input) { return isMatch(input, REGEX_URL); }
    }
}
