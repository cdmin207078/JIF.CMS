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
            _workContext = workContext;

            _attachmentRootPath = Server.MapPath("../attachments");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // 大文件上传之前, 判断是否是断点续传
        [HttpGet]
        public JsonResult BigFilePreCheck(string fname, int fsize, string lastModifiedDate)
        {
            var algo = EncyptHelper.CreateHashAlgoMd5();
            var plain = string.Format("{0}-{1}-{2}-{3}", _workContext.CurrentUser.Account, fname, fsize, lastModifiedDate);
            var cipher = EncyptHelper.Encrypt(algo, plain);

            if (Directory.Exists(Path.Combine(_attachmentRootPath, cipher)))
            {
                return AjaxOk(Uploadmode.Continued);
            }
            else
            {
                return AjaxOk(Uploadmode.New);
            }
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var file = Request.Files[0];

            // 判断路径是否已经创建
            if (false == Directory.Exists(_attachmentRootPath))
            {
                Directory.CreateDirectory(_attachmentRootPath);
            }

            // 检查上传模式 首次上传 \ 断点续传 \ 秒传

            if (string.IsNullOrWhiteSpace(Request["chunks"]))
            {
                var filepath = Path.Combine(_attachmentRootPath, file.FileName);
                file.SaveAs(filepath);
            }
            else
            {
                var chunk = Request["chunk"];                 // 当前分片批次
                var chunks = int.Parse(Request["chunks"]);    // 分片总数
                var chunksuffix = "pr." + chunk;            // 分片文件后缀名

                var fn = string.Format("{0}.{1}", file.FileName, chunksuffix);
                var filepath = Path.Combine(_attachmentRootPath, fn);

                //lock (_locker)
                //{
                //Thread.Sleep(new Random(1).Next(1000, 3000));
                file.SaveAs(filepath);
                //}


                //mergeFile(file.FileName, rootPath, chunks);
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
            var fns = Directory.GetFiles(rootPath).Where(d => d.Contains(filename)).OrderBy(d => Convert.ToInt32(d.Substring(d.LastIndexOf(".") + 1)));
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