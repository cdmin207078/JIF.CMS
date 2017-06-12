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
    }
}
