﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace JIF.CMS.Test.Core
{
    [TestClass]
    public class FileOperateTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            var files = new List<string>();

            files.Add("VisualStudio2017_ProductLaunchPoster-1.png.temp.3");
            files.Add("VisualStudio2017_ProductLaunchPoster-1.png.temp.0");
            files.Add("VisualStudio2017_ProductLaunchPoster-1.png.temp.2");
            files.Add("VisualStudio2017_ProductLaunchPoster-1.png.temp.4");
            files.Add("VisualStudio2017_ProductLaunchPoster-1.png.temp.1");

            Console.WriteLine("===============  原始  ====================");

            foreach (var item in files)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("===============  排序 - 升序  ====================");

            files.Sort();

            foreach (var item in files)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("===============  排序 - 降序  ====================");

            files = files.OrderByDescending(d => d).ToList();

            foreach (var item in files)
            {
                Console.WriteLine(item);
            }

        }


        [TestMethod]
        public void CreateFileTest()
        {
            var fn = @"F:\WorkDocument\JIF.CMS\code\JIF.CMS\JIF.CMS.Management\attachments\1.jpg";
            var fnCopy = @"F:\WorkDocument\JIF.CMS\code\JIF.CMS\JIF.CMS.Management\attachments\2.jpg";

            using (var fs = System.IO.File.Create(fnCopy))
            {

                var fnContent = System.IO.File.ReadAllBytes(fn);

                fs.Write(fnContent, 0, fnContent.Count());
            }
        }

        [TestMethod]
        public void CreateAndWriteTextTest()
        {
            var fn = @"E:\JIF.CMS\code\JIF.CMS\JIF.CMS.Management\attachments\num.txt";

            using (var fs = System.IO.File.Create(fn))
            {
                for (int i = 1; i <= 500000; i++)
                {
                    var v = Encoding.Default.GetBytes(i.ToString() + ",");
                    fs.Write(v, 0, v.Count());
                }
            }
        }


        [TestMethod]
        public void Write_WriteByte_Speed_Test()
        {
            var f1 = @"F:\WorkDocument\JIF.CMS\code\JIF.CMS\JIF.CMS.Management\attachments\1.png";
            var f2 = @"F:\WorkDocument\JIF.CMS\code\JIF.CMS\JIF.CMS.Management\attachments\2.png";
            var f3 = @"F:\WorkDocument\JIF.CMS\code\JIF.CMS\JIF.CMS.Management\attachments\3.png";

            Stopwatch watch = new Stopwatch();

            //for (int i = 0; i < 2; i++)
            {
                watch.Restart();

                using (var fs = System.IO.File.Create(f2))
                {
                    var fc = System.IO.File.ReadAllBytes(f1);

                    fs.Write(fc, 0, fc.Length);

                }

                watch.Stop();

                Console.WriteLine($"Write - 耗时: { watch.ElapsedMilliseconds    }___.");


                watch.Restart();

                using (var fs = System.IO.File.Create(f3))
                {
                    var fc = System.IO.File.ReadAllBytes(f1);

                    foreach (var c in fc)
                    {
                        fs.WriteByte(c);
                    }
                }
                watch.Stop();
                Console.WriteLine($"WriteByte - 耗时: { watch.ElapsedMilliseconds    }___.");
            }
        }
    }
}