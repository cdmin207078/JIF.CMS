using JIF.CMS.Core;
using JIF.CMS.Core.Helpers;
using JIF.CMS.Web.Framework.Controllers;
using JIF.CMS.Web.Framework.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace JIF.CMS.Management.Controllers
{
    public class AttachmentController : AdminControllerBase
    {
        private static object _locker = new object();

        private readonly string _attachmentRootPath;

        private readonly IWorkContext _workContext;

        public AttachmentController(IWorkContext workContext)
        {
            _attachmentRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "attachments");

            _workContext = workContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        private string BigFileChunkFolder(string fname, long fsize, long lastModifiedTimestamp)
        {
            var algo = EncyptHelper.CreateHashAlgoMd5();
            var plain = string.Format("{0}-{1}-{2}-{3}", _workContext.CurrentUser.Account, fname, fsize, lastModifiedTimestamp);
            var cipher = EncyptHelper.Encrypt(algo, plain);

            return Path.Combine(_attachmentRootPath, cipher);
        }

        // 大文件上传之前, 判断是否是断点续传
        [HttpGet]
        public JsonResult BigFilePreCheck(string fname, long fsize, long lastModifiedTimestamp)
        {
            var chunkFolder = BigFileChunkFolder(fname, fsize, lastModifiedTimestamp);

            if (Directory.Exists(chunkFolder))
            {
                var chunks = string.Join(",", Directory.GetFiles(Path.Combine(_attachmentRootPath, chunkFolder))
                    .Select(d => d.Substring(d.LastIndexOf('\\') + 1)).ToArray());

                return AjaxOk(new { mode = Uploadmode.Continued, chunks = chunks });
            }
            else
            {
                return AjaxOk(new { mode = Uploadmode.New });
            }
        }

        [HttpPost]
        public JsonResult Upload()
        {
            // 判断路径是否已经创建
            if (!Directory.Exists(_attachmentRootPath))
                Directory.CreateDirectory(_attachmentRootPath);


            var file = Request.Files[0];

            // 检查上传模式 首次上传 \ 断点续传 \ 秒传
            if (string.IsNullOrWhiteSpace(Request["chunks"]))
            {
                var filepath = Path.Combine(_attachmentRootPath, file.FileName);
                file.SaveAs(filepath);
            }
            else
            {
                var fname = Request["name"];
                var fsize = long.Parse(Request["size"]);
                var lastModifiedTimestamp = long.Parse(Request["lastModifiedDate"]);

                var chunkFolder = BigFileChunkFolder(fname, fsize, lastModifiedTimestamp);

                if (!Directory.Exists(chunkFolder))
                    Directory.CreateDirectory(chunkFolder);

                var chunk = Request["chunk"];                 // 当前分片批次

                var filepath = Path.Combine(chunkFolder, chunk);

                //lock (_locker)
                //{
                file.SaveAs(filepath);
                //}

                var chunks = int.Parse(Request["chunks"]);    // 分片总数
                mergeFile(file.FileName, chunkFolder, chunks);
            }

            return AjaxOk();
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
            var fns = Directory.GetFiles(rootPath).OrderBy(d => Convert.ToInt32(d.Substring(d.LastIndexOf("\\") + 1)));
            var segCount = fns.Count();

            if (chunks == segCount)
            {
                using (var fs = System.IO.File.Create(Path.Combine(rootPath, filename)))
                {
                    foreach (var fn in fns)
                    {
                        var segContent = System.IO.File.ReadAllBytes(fn);
                        fs.Write(segContent, 0, segContent.Length);

                        // 删除分片
                        System.IO.File.Delete(fn);
                    }
                }
            }
        }
    }
}