using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class EncodingHelperTest
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

        [TestMethod]
        public void Encoding_And_Base64_Study_Test()
        {
            var plain = "密";

            var bytesDefault = Encoding.Default.GetBytes(plain);
            var bytesASCII = Encoding.ASCII.GetBytes(plain);
            var bytesUTF8 = Encoding.UTF8.GetBytes(plain);
            var bytesUnicode = Encoding.Unicode.GetBytes(plain);
            var bytesGBK = Encoding.GetEncoding("GBK").GetBytes(plain);
            var bytesGB2312 = Encoding.GetEncoding("GB2312").GetBytes(plain);

            PrintBytes("Encoding.Default", bytesDefault);
            PrintBytes("Encoding.ASCII", bytesASCII);
            PrintBytes("Encoding.UTF8", bytesUTF8);
            PrintBytes("Encoding.Unicode", bytesUnicode);
            PrintBytes("Encoding.GBK", bytesGBK);
            PrintBytes("Encoding.GB2312", bytesGB2312);

            Console.WriteLine("Encoding.Default : " + Convert.ToBase64String(bytesDefault));
            Console.WriteLine("Encoding.ASCII : " + Convert.ToBase64String(bytesASCII));
            Console.WriteLine("Encoding.UTF8 : " + Convert.ToBase64String(bytesUTF8));

            Console.WriteLine("" + Convert.ToBase64String(bytesDefault));
            Console.WriteLine("" + Convert.ToBase64String(bytesASCII));
            Console.WriteLine("" + Convert.ToBase64String(bytesUTF8));
        }

        [TestMethod]
        public void Encode_Decode_Base64_Test()
        {
            var plain = "Man你好";

            var cipher = EncodeHelper.EncodeBase64(Encoding.UTF8, plain);
            var origin = EncodeHelper.DecodeBase64(Encoding.UTF8, cipher);

            Console.WriteLine(cipher);
            Console.WriteLine(origin);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var plain = "ZVpY1oAEBd4hcfU9ldd+d9y4dmMPNI8ZycNzr72pUdNfvbmncQcit/5rh9VCTNet";


            var bytes = Encoding.UTF8.GetBytes(plain);
            var text = Encoding.UTF8.GetString(bytes);

            var base64bytes = Convert.FromBase64String(plain);
            var base64Text = Convert.ToBase64String(base64bytes);

            Console.WriteLine(EncodeHelper.DecodeBase64(Encoding.UTF8, "5oKs5bSW"));
        }

        private void PrintBytes(string type, byte[] bytes)
        {
            Console.WriteLine(string.Format("{0}\t\t\t{1}", type, string.Join(",", bytes)));
        }
    }
}
