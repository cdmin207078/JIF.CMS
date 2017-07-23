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



        public void fff()
        {

            Product pro = new PP();


            Func<string, PP> ff = (x) =>{return null;}; 
            Func<string, Product> fff = ff;


            List<PP> ff1 = new List<PP>() { }; 
            //List<Product> fff1 = ff1; 




        }



    }


    public class PP : Product

    {
        public string Na { get; set; }

    }





}
