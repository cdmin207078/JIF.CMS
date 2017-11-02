using System;
using JIF.CMS.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
