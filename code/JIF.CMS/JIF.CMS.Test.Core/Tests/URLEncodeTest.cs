using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Text;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class URLEncodeTest
    {

        protected string UrlEncodeUpper(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
        }

        protected string UrlPathEncodeUpper(string s)
        {
            char[] temp = HttpUtility.UrlPathEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
        }

        [TestMethod]
        public void TestMethod1()
        {

            var c = "a%BC - 老司机, 杨主簿,";
            c = "百度,source,a%bC";
            c = "~!@#$%^&*()_+-={};'\\|:\",.<>/?";

            Console.WriteLine(c);

            Console.WriteLine("------------PathEncode-----------------");
            Console.WriteLine(HttpUtility.UrlPathEncode(c));
            Console.WriteLine(UrlPathEncodeUpper(c));
            Console.WriteLine(HttpUtility.UrlDecode(UrlPathEncodeUpper(c)));


            Console.WriteLine("------------Encode-----------------");
            Console.WriteLine(HttpUtility.UrlEncode(c));
            Console.WriteLine(UrlEncodeUpper(c));
            Console.WriteLine(HttpUtility.UrlDecode(UrlEncodeUpper(c)));


            var A = "%E5%BE%AE%E4%BF%A1";
            var a = "%e5%be%ae%e4%bf%a1";
            Console.WriteLine(string.Compare(A, a, true));


            var b = "All";
            Console.WriteLine(b);
            Console.WriteLine(HttpUtility.UrlDecode(b));


        }
    }
}
