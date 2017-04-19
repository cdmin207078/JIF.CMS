using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Text;

namespace JIF.CMS.Test.Core
{
    [TestClass]
    public class URLEncodeTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            var a = "腾讯,百度,阿里";

            var b = "%e8%85%be%e8%ae%af,%e7%99%be%e5%ba%a6,%e9%98%bf%e9%87%8c";

            Console.WriteLine(HttpUtility.UrlEncode(a));
            Console.WriteLine(HttpUtility.UrlPathEncode(a));
            Console.WriteLine(HttpUtility.JavaScriptStringEncode(a));



            Console.WriteLine(b);


            Console.WriteLine(HttpUtility.UrlDecode(b));

        }
    }
}
