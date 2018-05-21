using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class RegexHelperTest
    {
        [TestMethod]
        public void Is_Mobile_Test()
        {
            Console.WriteLine(RegexHelper.IsMobile("11111111111"));
            Console.WriteLine(RegexHelper.IsMobile("aaaaaaaaaaa"));
            Console.WriteLine(RegexHelper.IsMobile("+++++++++++"));

            // 移动
            Console.WriteLine(RegexHelper.IsMobile("13411111111"));
            Console.WriteLine(RegexHelper.IsMobile("13511111111"));
            Console.WriteLine(RegexHelper.IsMobile("13611111111"));
            Console.WriteLine(RegexHelper.IsMobile("13711111111"));
            Console.WriteLine(RegexHelper.IsMobile("13811111111"));
            Console.WriteLine(RegexHelper.IsMobile("13911111111"));
            Console.WriteLine(RegexHelper.IsMobile("14711111111"));
            Console.WriteLine(RegexHelper.IsMobile("15011111111"));
            Console.WriteLine(RegexHelper.IsMobile("15111111111"));
            Console.WriteLine(RegexHelper.IsMobile("15211111111"));
            Console.WriteLine(RegexHelper.IsMobile("15711111111"));
            Console.WriteLine(RegexHelper.IsMobile("15811111111"));
            Console.WriteLine(RegexHelper.IsMobile("15911111111"));
            Console.WriteLine(RegexHelper.IsMobile("18211111111"));
            Console.WriteLine(RegexHelper.IsMobile("18311111111"));
            Console.WriteLine(RegexHelper.IsMobile("18711111111"));
            Console.WriteLine(RegexHelper.IsMobile("18811111111"));

            // 联通
            Console.WriteLine(RegexHelper.IsMobile("13011111111"));
            Console.WriteLine(RegexHelper.IsMobile("13111111111"));
            Console.WriteLine(RegexHelper.IsMobile("13211111111"));
            Console.WriteLine(RegexHelper.IsMobile("15511111111"));
            Console.WriteLine(RegexHelper.IsMobile("15611111111"));
            Console.WriteLine(RegexHelper.IsMobile("18511111111"));
            Console.WriteLine(RegexHelper.IsMobile("18611111111"));
            Console.WriteLine(RegexHelper.IsMobile("14511111111"));

            // 电信
            Console.WriteLine(RegexHelper.IsMobile("13311111111"));
            Console.WriteLine(RegexHelper.IsMobile("15311111111"));
            Console.WriteLine(RegexHelper.IsMobile("17311111111"));
            Console.WriteLine(RegexHelper.IsMobile("17711111111"));
            Console.WriteLine(RegexHelper.IsMobile("18011111111"));
            Console.WriteLine(RegexHelper.IsMobile("18111111111"));
            Console.WriteLine(RegexHelper.IsMobile("18911111111"));
            Console.WriteLine(RegexHelper.IsMobile("19911111111"));
        }

        [TestMethod]
        public void Is_Email_Test()
        {
            Console.WriteLine("a" + "\t->\t" + RegexHelper.IsEmail("a"));
            Console.WriteLine("a@" + "\t->\t" + RegexHelper.IsEmail("a@"));
            Console.WriteLine("a@a" + "\t->\t" + RegexHelper.IsEmail("a@a"));
            Console.WriteLine("a@a." + "\t->\t" + RegexHelper.IsEmail("a@a."));
            Console.WriteLine("@a" + "\t->\t" + RegexHelper.IsEmail("@a"));
            Console.WriteLine("@a." + "\t->\t" + RegexHelper.IsEmail("@a."));
            Console.WriteLine("@a.a" + "\t->\t" + RegexHelper.IsEmail("@a.a"));

            Console.WriteLine("@a.1" + "\t->\t" + RegexHelper.IsEmail("@a.1"));
            Console.WriteLine("@1.a" + "\t->\t" + RegexHelper.IsEmail("@1.a"));
            Console.WriteLine("@1.1" + "\t->\t" + RegexHelper.IsEmail("@1.1"));
            Console.WriteLine("1@1.1" + "\t->\t" + RegexHelper.IsEmail("1@1.1"));

            Console.WriteLine("a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("a@a.com.1"));
            Console.WriteLine(".a@a.com" + "\t->\t" + RegexHelper.IsEmail(".a@a.com"));
            Console.WriteLine("1a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("1a@a.com.1"));
            Console.WriteLine("$a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("$a@a.com.1"));
            Console.WriteLine("\a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("\a@a.com.1"));
            Console.WriteLine("\\a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("\\a@a.com.1"));
            Console.WriteLine("\'a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("\'a@a.com.1"));
            Console.WriteLine("\"a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("\"a@a.com.1"));
            Console.WriteLine("'a@a.com.1" + "\t->\t" + RegexHelper.IsEmail("'a@a.com.1"));
            Console.WriteLine(@"""Ima.Fool""@example.com" + "\t->\t" + RegexHelper.IsEmail(@"""Ima.Fool""@example.com"));
            Console.WriteLine("a@a.a" + "\t->\t" + RegexHelper.IsEmail("a@a.a"));


            Console.WriteLine("1a@a.aa" + "\t->\t" + RegexHelper.IsEmail("1a@a.aa"));
            Console.WriteLine("-a@a.aa" + "\t->\t" + RegexHelper.IsEmail("-a@a.aa"));
            Console.WriteLine("_a@a.aa" + "\t->\t" + RegexHelper.IsEmail("_a@a.aa"));
            Console.WriteLine("=a@a.aa" + "\t->\t" + RegexHelper.IsEmail("=a@a.aa"));
            Console.WriteLine("+a@a.aa" + "\t->\t" + RegexHelper.IsEmail("+a@a.aa"));
            Console.WriteLine("!a@a.aa" + "\t->\t" + RegexHelper.IsEmail("!a@a.aa"));
            Console.WriteLine("%a@a.aa" + "\t->\t" + RegexHelper.IsEmail("%a@a.aa"));
            Console.WriteLine("#a@a.aa" + "\t->\t" + RegexHelper.IsEmail("#a@a.aa"));

            Console.WriteLine("a1a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a1a@a.aa"));
            Console.WriteLine("a-a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a-a@a.aa"));
            Console.WriteLine("a_a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a_a@a.aa"));
            Console.WriteLine("a=a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a=a@a.aa"));
            Console.WriteLine("a+a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a+a@a.aa"));
            Console.WriteLine("a!a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a!a@a.aa"));
            Console.WriteLine("a%a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a%a@a.aa"));
            Console.WriteLine("a#a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a#a@a.aa"));


            Console.WriteLine("a@a.aa" + "\t->\t" + RegexHelper.IsEmail("a@a.aa"));

        }
    }
}