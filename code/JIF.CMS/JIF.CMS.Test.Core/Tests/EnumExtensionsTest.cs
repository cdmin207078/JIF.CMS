using System;
using JIF.CMS.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class EnumExtensionsTest
    {
        [TestMethod]
        public void Enum_Class_Method_Test()
        {
            // 获取枚举所有值
            var names = Enum.GetNames(typeof(EnumExtensionsType));
            var values = Enum.GetValues(typeof(EnumExtensionsType));

            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine("{0} = {1}", names[i], values.GetValue(i));
            }

            // 根据数值, 获取指定枚举名称
            Console.WriteLine(Enum.GetName(typeof(EnumExtensionsType), 5)); // 不存在枚举值, 返回空字符串
            Console.WriteLine(Enum.GetName(typeof(EnumExtensionsType), 3));

        }

        [TestMethod]
        public void GetDescription_Test()
        {
            Console.WriteLine(EnumExtensionsType.Android);
            Console.WriteLine(EnumExtensionsType.Android.ToString());
            Console.WriteLine(EnumExtensionsType.Android.GetDescription());

            Console.WriteLine(EnumExtensionsType.BlackBerry);
            Console.WriteLine(EnumExtensionsType.BlackBerry.GetDescription());
        }

        [TestMethod]
        public void MyTestMethod()
        {
            Console.WriteLine(Convert.ToBoolean(-1));
            Console.WriteLine(Convert.ToBoolean(0));
            Console.WriteLine(Convert.ToBoolean(1));

            Console.WriteLine(Convert.ToBoolean(1.2));


            var a = "1";
            var b = EnumExtensionsType.Android;

            var c = Enum.Parse(typeof(EnumExtensionsType), "1");

            Console.WriteLine(c);


            Console.WriteLine(a.GetType().IsEnum);
            Console.WriteLine(b.GetType().IsEnum);
        }

        [TestMethod]
        public void Enum_Prop_Serialize_Test()
        {
            var a = new
            {
                id = 1,
                name = "Hello world",
                EE = EnumExtensionsType.WP
            };

            Console.WriteLine(JsonConvert.SerializeObject(a));
        }

        [TestMethod]
        public void Chinese_Enum_Test()
        {
            var rel = ContactRelation.兄弟;

            Console.WriteLine(rel);
            Console.WriteLine(rel.GetDescription());
            Console.WriteLine(Convert.ToInt32(rel));
        }

        enum EnumExtensionsType
        {
            [System.ComponentModel.Description("谷歌")]
            Android,

            [System.ComponentModel.Description("苹果")]
            IOS,

            [System.ComponentModel.Description("微软")]
            WP,

            BlackBerry
        }


        enum ContactRelation
        {
            [System.ComponentModel.Description("父亲")]
            父亲 = 0,

            [System.ComponentModel.Description("母亲")]
            母亲 = 1,

            [System.ComponentModel.Description("儿子")]
            儿子 = 2,

            [System.ComponentModel.Description("女儿")]
            女儿 = 3,

            [System.ComponentModel.Description("兄弟")]
            兄弟 = 4,

            [System.ComponentModel.Description("姐妹")]
            姐妹 = 5
        }
    }
}
