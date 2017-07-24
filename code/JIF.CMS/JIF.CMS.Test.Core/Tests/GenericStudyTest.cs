using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Test.Core.Entities;
using System.Collections.Generic;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class GenericStudyTest
    {

        [TestMethod]
        public void recursiveSetPriceTest()
        {

            var source = new List<Product>() {
                new Product { SysNo = 1, Price = 1 },
                new Product { SysNo = 2, Price = 2 }
            };

            var max = 0;
            recursiveSetPrice(source, ref max);

            foreach (var p in source)
            {
                Console.WriteLine(string.Format("sysno: {0}, price: {1}", p.SysNo, p.Price));
            }
        }

        private void recursiveSetPrice(List<Product> source, ref int max)

        {
            if (max > 5)
                return;

            foreach (var p in source)
            {
                p.Price++;
            }

            max++;

            var ss = new List<Product>() {
                new Product { SysNo = 1, Price = 1 },
                new Product { SysNo = 2, Price = 2 }
            };

            recursiveSetPrice(ss, ref max);
        }



        [TestMethod]
        public void Covariant_Contravariant_Test()
        {

            IEnumerable<Product> products = new List<Box>() {
                new Box { SysNo = 1,Color = "red" },
                new Box { SysNo = 2, Color = "green" },
                new Box { SysNo = 3, Color= "dark" },
            };

            //show_nibian((List<Box>)products);


            List<Box> boxs = new List<Box>
            {
                 new Box { SysNo = 1, Color="yellow"},
                 new Box { SysNo = 2, Color="blue"},
                 new Box { SysNo = 3, Color= "purple"},
            };


            //show_xiebian(boxs);


            var result = boxs.xiebian();



            //foreach (var item in result)
            //{
            //    Console.WriteLine("no: {0}, color: {1}", item.SysNo, item.Color);

            //}


            //IEnumerable<Product> aa = null;
            //IEnumerable<Box> bb = null;


            //IList<Product> cc = null;
            //IList<Box> dd = null;

            //aa = bb;  // 协变

            //bb = aa;  // 


            //cc = aa;
            //aa = cc;
            //aa = dd;
            //bb = cc;
            //bb = dd;

            //cc = dd;
            //dd = cc;
        }


        private void show_nibian(List<Box> boxs)
        {
            foreach (var b in boxs)
            {
                Console.WriteLine("no: {0}, color: {1}", b.SysNo, b.Color);
            }
        }


        private void show_xiebian(IEnumerable<Product> products)
        {
            foreach (var p in products)
            {

                var b = (Box)p;

                Console.WriteLine("no: {0}, color: {1}", b.SysNo, b.Color);
            }
        }
    }


    public static class ExtendsClass
    {
        public static IEnumerable<Product> xiebian(this IEnumerable<Product> boxs)
        {
            foreach (var b in boxs)
            {
                Console.WriteLine("no: {0}", b.SysNo);
            }

            var result = boxs.ToList();

            return result;
        }
    }

    public class Box : Product

    {
        public string Color { get; set; }
    }





}
