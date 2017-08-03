using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Text;
using System.Collections.Generic;
using JIF.CMS.Test.Core.Entities;
using System.Linq;

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


        [TestMethod]
        public void MyTestMethod()
        {
            var source = new List<Product>();
            source.Add(new Product { SysNo = 1, ProductId = "P001", CreateTime = DateTime.Now, Price = 10.3m });
            source.Add(new Product { SysNo = 2, ProductId = "P002", CreateTime = DateTime.Now, Price = 13.2m });
            source.Add(new Product { SysNo = 3, ProductId = "P003", CreateTime = DateTime.Now, Price = 17.3m });
            source.Add(new Product { SysNo = 4, ProductId = "P004", CreateTime = DateTime.Now, Price = 15.4m });
            source.Add(new Product { SysNo = 5, ProductId = "P005", CreateTime = DateTime.Now, Price = 1.5m });

            var target = new List<Product>();
            target.Add(new Product { SysNo = 2, ProductId = "P002", CreateTime = DateTime.Now, Price = 13.2m });
            target.Add(new Product { SysNo = 3, ProductId = "P003", CreateTime = DateTime.Now, Price = 17.3m });
            target.Add(new Product { SysNo = 4, ProductId = "P004", CreateTime = DateTime.Now, Price = 15.4m });
            target.Add(new Product { SysNo = 6, ProductId = "P004", CreateTime = DateTime.Now, Price = 15.4m });

            var all = target.All(t => source.Any(s => s.SysNo == t.SysNo));
            Console.WriteLine(all);
        }


        [TestMethod]
        public void Test_JS_Esacpe_Html()
        {
            var s = "";
            Console.WriteLine(s);

            Console.WriteLine(HttpUtility.UrlDecode(s));
            Console.WriteLine(HttpUtility.UrlDecode(HttpUtility.UrlDecode(s)));

            Console.WriteLine(HttpUtility.HtmlDecode(s));


            var ss = @"&lt;p&gt;&lt;img alt=&quot;Image&quot; src=&quot;http://img.xiami.net/images/album/img46/1046/5619561472119930.jpg&quot; width=&quot;707&quot; height=&quot;707&quot;&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;纵有红颜 百生千劫&lt;/p&gt;&lt;p&gt;难消君心 万古情愁&lt;/p&gt;&lt;p&gt;青峰之巅 山外之山&lt;/p&gt;&lt;p&gt;晚霞寂照 星夜无眠&lt;/p&gt;&lt;p&gt;&amp;nbsp;&lt;/p&gt;&lt;p&gt;如幻大千 惊鸿一瞥&lt;/p&gt;&lt;p&gt;一曲终了 悲欣交集&lt;/p&gt;&lt;p&gt;夕阳之间 天外之天&lt;/p&gt;&lt;p&gt;梅花清幽 独立春寒&lt;/p&gt;&lt;p&gt;&amp;nbsp;&lt;/p&gt;&lt;p&gt;红尘中 你的无上清凉&lt;/p&gt;&lt;p&gt;寂静光明 默默照耀世界&lt;/p&gt;&lt;p&gt;行如风 如君一骑绝尘&lt;/p&gt;&lt;p&gt;空谷绝响 至今谁在倾听&lt;/p&gt;&lt;p&gt;&amp;nbsp;&lt;/p&gt;&lt;p&gt;一念净心 花开遍世界&lt;/p&gt;&lt;p&gt;每临绝境 峰回路又转&lt;/p&gt;&lt;p&gt;但凭净信 自在出乾坤&lt;/p&gt;&lt;p&gt;恰似如梦初醒 归途在眼前&lt;/p&gt;&lt;p&gt;&amp;nbsp;&lt;/p&gt;&lt;p&gt;行尽天涯 静默山水间&lt;/p&gt;&lt;p&gt;倾听晚风 拂柳笛声残&lt;/p&gt;&lt;p&gt;踏破芒鞋 烟雨任平生&lt;/p&gt;&lt;p&gt;慧行坚勇 究畅恒无极&lt;/p&gt;";

            Console.WriteLine(HttpUtility.UrlDecode(ss));
            Console.WriteLine(HttpUtility.HtmlDecode(ss));

        }


        [TestMethod]
        public void MyTestMethod1111123()
        {
            //var s = "Hello World";


            //Console.WriteLine(HttpUtility.UrlPathEncode(s));
            //Console.WriteLine(UrlPathEncodeUpper(s));

            //var url = "C+ +/c#";
            var url = "利乐®A3冰淇淋灌装机";

            var enurl = "%E5%88%A9%E4%B9%90%C2%AEA3%E5%86%B0%E6%B7%87%E6%B7%8B%E7%81%8C%E8%A3%85%E6%9C%BA";
            Console.WriteLine(enurl.Length);

            Console.WriteLine(HttpUtility.UrlEncode(url));
            Console.WriteLine(HttpUtility.UrlDecode(HttpUtility.UrlEncode(url)));

            Console.WriteLine(HttpUtility.UrlPathEncode(url));
            Console.WriteLine(HttpUtility.UrlDecode(HttpUtility.UrlPathEncode(url)));

            Console.WriteLine(HttpUtility.HtmlEncode(url));
            Console.WriteLine(HttpUtility.HtmlDecode(HttpUtility.HtmlEncode(url)));

            Console.WriteLine(HttpUtility.HtmlAttributeEncode(url));
            Console.WriteLine(HttpUtility.UrlDecode(HttpUtility.HtmlAttributeEncode(url)));

            Console.WriteLine(HttpUtility.JavaScriptStringEncode(url));
            Console.WriteLine(HttpUtility.UrlDecode(HttpUtility.JavaScriptStringEncode(url)));


            Console.WriteLine(Uri.EscapeDataString(url));
        }
    }
}
