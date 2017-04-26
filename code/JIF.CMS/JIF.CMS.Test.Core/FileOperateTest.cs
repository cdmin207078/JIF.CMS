using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
            var ff = @"E:\JIF.CMS\code\JIF.CMS\JIF.CMS.Management\attachments";
            var fr = "VisualStudio2017_ProductLaunchPoster-1.png";
            var ftsuffix = ".temp.";

            var f1 = Path.Combine(ff, fr);
            var f2 = Path.Combine(ff, "Wrtie-整体写入.png");
            var f3 = Path.Combine(ff, "WriteByte-整体写入.png");
            var f4 = Path.Combine(ff, "Wrtie-分片写入.png");
            var f5 = Path.Combine(ff, "WriteByte-分片写入.png");

            var fns = Directory.GetFiles(ff).Where(d => d.Contains(ftsuffix)).OrderBy(d => Convert.ToInt32(d.Substring(d.LastIndexOf(".") + 1)));


            var watch = new Stopwatch();

            watch.Start();

            using (var fs = File.Create(f2))
            {
                var oc = File.ReadAllBytes(f1);

                fs.Write(oc, 0, oc.Length);
            }

            Console.WriteLine($"完整文件, Write 写入耗时: {watch.ElapsedMilliseconds}");
            watch.Restart();

            using (var fs = File.Create(f3))
            {
                var oc = File.ReadAllBytes(f1);

                foreach (var c in oc)
                {
                    fs.WriteByte(c);
                }
            }

            Console.WriteLine($"完整文件, WriteByte 写入耗时: {watch.ElapsedMilliseconds}");
            watch.Restart();



            using (var fs = File.Create(f4))
            {
                foreach (var fn in fns)
                {
                    var oc = System.IO.File.ReadAllBytes(fn);
                    fs.Write(oc, 0, oc.Length);
                }
            }

            Console.WriteLine($"分片文件, Write 写入耗时: {watch.ElapsedMilliseconds}");
            watch.Restart();


            using (var fs = File.Create(f5))
            {
                foreach (var fn in fns)
                {
                    var oc = System.IO.File.ReadAllBytes(fn);
                    foreach (var c in oc)
                    {
                        fs.WriteByte(c);
                    }
                }
            }

            Console.WriteLine($"分片文件, WriteByte 写入耗时: {watch.ElapsedMilliseconds}");
            watch.Restart();

            watch.Stop();

        }
    }
}