using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class EncodingTest
    {
        [TestMethod]
        public void String_To_Bytes_Test()
        {
            var a = "Hello";

            PrintBytes("Default", Encoding.Default.GetBytes(a));
            PrintBytes("ASCII", Encoding.ASCII.GetBytes(a));
            PrintBytes("UTF8", Encoding.UTF8.GetBytes(a));
            PrintBytes("UTF7", Encoding.UTF7.GetBytes(a));
            PrintBytes("UTF32", Encoding.UTF32.GetBytes(a));
            PrintBytes("Unicode", Encoding.Unicode.GetBytes(a));
            PrintBytes("BigEndianUnicode", Encoding.BigEndianUnicode.GetBytes(a));


            a = "你好";

            PrintBytes("Default", Encoding.Default.GetBytes(a));
            PrintBytes("ASCII", Encoding.ASCII.GetBytes(a));
            PrintBytes("UTF8", Encoding.UTF8.GetBytes(a));
            PrintBytes("UTF7", Encoding.UTF7.GetBytes(a));
            PrintBytes("UTF32", Encoding.UTF32.GetBytes(a));
            PrintBytes("Unicode", Encoding.Unicode.GetBytes(a));
            PrintBytes("BigEndianUnicode", Encoding.BigEndianUnicode.GetBytes(a));

            a = "नमस्ते";
            PrintBytes("Default", Encoding.Default.GetBytes(a));
            PrintBytes("ASCII", Encoding.ASCII.GetBytes(a));
            PrintBytes("UTF8", Encoding.UTF8.GetBytes(a));
            PrintBytes("UTF7", Encoding.UTF7.GetBytes(a));
            PrintBytes("UTF32", Encoding.UTF32.GetBytes(a));
            PrintBytes("Unicode", Encoding.Unicode.GetBytes(a));
            PrintBytes("BigEndianUnicode", Encoding.BigEndianUnicode.GetBytes(a));

        }

        private void PrintBytes(string type, byte[] bytes)
        {
            Console.WriteLine(string.Format("{0}\t\t\t{1}", type, string.Join(",", bytes)));
        }
    }
}
