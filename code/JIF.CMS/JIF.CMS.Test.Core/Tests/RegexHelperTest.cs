using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class RegexHelperTest
    {
        class RegexWarpper
        {
            private Func<string, bool> _regex { get; set; }

            public RegexWarpper(Func<string, bool> regex)
            {
                _regex = regex;
            }

            public void Test(string input)
            {
                Console.WriteLine(input + "\t->\t" + this._regex(input));
            }
        }

        [TestMethod]
        public void Is_Mobile_Test()
        {
            RegexWarpper rw = new RegexWarpper(RegexHelper.IsMobile);

            rw.Test("11111111111");
            rw.Test("aaaaaaaaaaa");
            rw.Test("+++++++++++");
            rw.Test("013311111111");

            Console.WriteLine("-----------电信号段---------------");
            rw.Test("13311111111");
            rw.Test("14911111111");
            rw.Test("15311111111");
            rw.Test("17311111111");
            rw.Test("17711111111");
            rw.Test("18011111111");
            rw.Test("18111111111");
            rw.Test("18911111111");
            rw.Test("19911111111");

            Console.WriteLine("-----------联通号段---------------");
            rw.Test("13011111111");
            rw.Test("13111111111");
            rw.Test("13211111111");
            rw.Test("14511111111");
            rw.Test("15511111111");
            rw.Test("15611111111");
            rw.Test("16611111111");
            rw.Test("17111111111");
            rw.Test("17511111111");
            rw.Test("17611111111");
            rw.Test("18511111111");
            rw.Test("18611111111");

            Console.WriteLine("-----------移动号段---------------");
            rw.Test("13411111111");
            rw.Test("13511111111");
            rw.Test("13611111111");
            rw.Test("13711111111");
            rw.Test("13811111111");
            rw.Test("13911111111");
            rw.Test("14711111111");
            rw.Test("15011111111");
            rw.Test("15111111111");
            rw.Test("15211111111");
            rw.Test("15711111111");
            rw.Test("15811111111");
            rw.Test("15911111111");
            rw.Test("17811111111");
            rw.Test("18211111111");
            rw.Test("18311111111");
            rw.Test("18411111111");
            rw.Test("18711111111");
            rw.Test("18811111111");
            rw.Test("19811111111");
        }

        [TestMethod]
        public void Is_Email_Test()
        {
            RegexWarpper rw = new RegexWarpper(RegexHelper.IsEmail);

            rw.Test("a");
            rw.Test("a@");
            rw.Test("a@a");
            rw.Test("a@a.");
            rw.Test("@a");
            rw.Test("@a.");
            rw.Test("@a.a");

            rw.Test("@a.1");
            rw.Test("@1.a");
            rw.Test("@1.1");
            rw.Test("1@1.1");

            rw.Test("a@a.com.1");
            rw.Test(".a@a.com");
            rw.Test("1a@a.com.1");
            rw.Test("$a@a.com.1");
            rw.Test("\a@a.com.1");
            rw.Test("\\a@a.com.1");
            rw.Test("\'a@a.com.1");
            rw.Test("\"a@a.com.1");
            rw.Test("'a@a.com.1");
            rw.Test(@"""Ima.Fool""@example.com");
            rw.Test("a@a.a");


            rw.Test("1a@a.aa");
            rw.Test("-a@a.aa");
            rw.Test("_a@a.aa");
            rw.Test("=a@a.aa");
            rw.Test("+a@a.aa");
            rw.Test("!a@a.aa");
            rw.Test("%a@a.aa");
            rw.Test("#a@a.aa");

            rw.Test("a1a@a.aa");
            rw.Test("a-a@a.aa");
            rw.Test("a_a@a.aa");
            rw.Test("a=a@a.aa");
            rw.Test("a+a@a.aa");
            rw.Test("a!a@a.aa");
            rw.Test("a%a@a.aa");
            rw.Test("a#a@a.aa");


            rw.Test("a@a.aa");

        }

        [TestMethod]
        public void Is_Tel_Test()
        {

        }

        [TestMethod]
        public void Is_IP_Test()
        {
            RegexWarpper rw = new RegexWarpper(RegexHelper.IsIPV4);
            rw.Test("1.1.1");
            rw.Test("1.1.1");
            rw.Test(".1.1.");
            rw.Test("1.1");
            rw.Test("1");
            rw.Test("''");
            rw.Test("0.0.0.0");
            rw.Test("1.1.1.1");
            rw.Test("-1.1.1.1");
            rw.Test("255.1.1.1");
            rw.Test("1.255.1.1");
            rw.Test("1.1.255.1");
            rw.Test("1.1.1.255");
            rw.Test("256.1.1.1");
            rw.Test("1.256.1.1");
            rw.Test("1.1.256.1");
            rw.Test("1.1.1.256");
            rw.Test("127.0.0.1");
            rw.Test("192.168.0.1");
            rw.Test("255.255.255.255");
            rw.Test("255.255.255.256");
            rw.Test("255.255.255.256");
            rw.Test("255.255.256.256");
            rw.Test("255.256.256.256");
            rw.Test("259.256.256.256");

        }

        [TestMethod]
        public void Is_URL_Test()
        {

        }
    }

}