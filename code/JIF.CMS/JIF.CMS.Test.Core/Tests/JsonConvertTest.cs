using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class JsonConvertTest
    {
        [TestMethod]
        public void Simple_Type_Json()
        {
            var a = "\"A\"";
            var b = "b";
            var c = 1.1;

            Console.WriteLine(a);
            Console.WriteLine(JsonConvert.SerializeObject(a));
            Console.WriteLine(JsonConvert.DeserializeObject(a));


            //Console.WriteLine(b);
            //Console.WriteLine(JsonConvert.SerializeObject(b));
            //Console.WriteLine(JsonConvert.DeserializeObject(b));


            Console.WriteLine(c);
            Console.WriteLine(JsonConvert.SerializeObject(c));
            Console.WriteLine(JsonConvert.DeserializeObject(c.ToString()));

        }
    }
}
