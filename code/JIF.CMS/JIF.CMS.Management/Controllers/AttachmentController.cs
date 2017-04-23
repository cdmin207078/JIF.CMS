using JIF.CMS.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.CMS.Management.Controllers
{
    public class AttachmentController : AdminControllerBase
    {
        private static object _locker = new object();


        // GET: Attachment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var file = Request.Files[0];
            var rootPath = Server.MapPath("../attachments");

            // 判断路径是否已经创建
            if (false == Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            if (string.IsNullOrWhiteSpace(Request["chunks"]))
            {
                var filepath = Path.Combine(rootPath, file.FileName);
                file.SaveAs(filepath);
            }
            else
            {
                var chunk = Request["chunk"];                 // 当前分片批次
                var chunks = int.Parse(Request["chunks"]);    // 分片总数
                var chunksuffix = "temp." + chunk;            // 分片文件后缀名

                var fn = string.Format("{0}.{1}", file.FileName, chunksuffix);
                var filepath = Path.Combine(rootPath, fn);

                lock (_locker)
                {
                    file.SaveAs(filepath);
                }

                mergeFile(file.FileName, rootPath, chunks);
            }

            return Json("OK");
        }

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="filename">上传文件名称</param>
        /// <param name="rootPath">文件保存根路径</param>
        /// <param name="chunks">合并文件总批次</param>
        private void mergeFile(string filename, string rootPath, int chunks)
        {
            // 上传完成合并文件
            var fns = Directory.GetFiles(rootPath).Where(d => d.Contains(filename)).OrderByDescending(d => d);
            var segCount = fns.Count();

            if (chunks == segCount)
            {
                using (var fs = System.IO.File.Create(Path.Combine(rootPath, filename)))
                {
                    foreach (var fn in fns)
                    {
                        var segContent = System.IO.File.ReadAllBytes(fn);

                        fs.Write(segContent, 0, segContent.Count());
                    }
                }
            }
        }
    }
}