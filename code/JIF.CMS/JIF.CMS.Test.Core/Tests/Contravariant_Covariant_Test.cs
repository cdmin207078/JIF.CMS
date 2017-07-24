using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using JIF.CMS.Test.Core.Entities;

namespace JIF.CMS.Test.Core.Tests
{
    /*
     * http://www.cnblogs.com/lemontea/archive/2013/02/17/2915065.html - 逆变与协变详解
     * 
     */

    [TestClass]
    public class Contravariant_Covariant_Test
    {
        public delegate T M1<T>();      // 不支持 逆变 与 协变
        public delegate T M2<out T>();  // 支持协变

        //泛型接口
        public interface IA<T> { }      //不支持逆变与协变
        public interface IB<out T>
        {
            //T get(T o);

        }  //支持协变


        [TestMethod]
        public void TestMethod1()
        {

            // 协变（Foo<父类> = Foo<子类> ）

            M1<object> a = null;
            M1<string> b = null;
            M2<object> c = null;
            M2<string> d = null;


            //a = b;      // 编译失败, 不支持逆变协变

            c = d;      // 支持协变


            IA<object> ia = null;
            IA<string> ib = null;
            IB<object> ic = null;
            IB<string> id = null;


            //ia = ib;    // 编译失败, 不支持逆变协变

            ic = id;    // 支持协变


            //数组：
            string[] strings = new string[] { "string" };

            object[] objects = strings;


        }


        public delegate void M3<T>();      // 不支持逆变 与 协变
        public delegate void M4<in T>();   // 支持逆变

        //泛型接口
        public interface IC<T> { }      //不支持逆变与协变
        public interface ID<in T>
        {

            //T get();

        }  //支持逆变


        [TestMethod]
        public void Contravariant_Test()
        {

            // 逆变: 逆变（Foo<子类> = Foo<父类>）


            M3<object> a = null;
            M3<string> b = null;
            M4<object> c = null;
            M4<string> d = null;

            // b = a;   //不支持逆变与协变

            d = c;      // 支持逆变



            IC<object> ia = null;
            IC<string> ib = null;
            ID<object> ic = null;
            ID<string> id = null;

            //ib = ia;

            id = ic;


            IEnumerable<object> ie = null;
            IEnumerable<string> ig = null;

            //ig = ie;  // 不支持逆变
            ie = ig;  // 支持协变


            IEnumerable<object> data = new List<Product> {
                new Product { SysNo = 1, Price = 20m }
            };

            IEnumerable<Product> source = null;

            //data = source;

            source = (IEnumerable<Product>)data;

            ie = source;
        }


    }
}
