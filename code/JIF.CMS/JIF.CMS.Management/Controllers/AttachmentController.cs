using JIF.CMS.Core;
using JIF.CMS.Core.Helpers;
using JIF.CMS.Services.Attachments;
using JIF.CMS.Services.Attachments.Dtos;
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
        private readonly string _attachmentRootPath;

        private readonly IWorkContext _workContext;

        private IAttachmentService _attachmentService;

        public AttachmentController(IWorkContext workContext
            , IAttachmentService attachmentService)
        {
            _attachmentRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "attachments");

            _attachmentService = attachmentService;
            _workContext = workContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得大文件分片存储文件目录
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="fsize"></param>
        /// <param name="lastModifiedTimestamp"></param>
        /// <returns></returns>
        private string getBigFileChunkFolder(string fname, long fsize, long lastModifiedTimestamp)
        {
            var algo = EncyptHelper.CreateHashAlgoMd5();

            var plain = string.Format("{0}-{1}-{2}-{3}", _workContext.CurrentUser.Account, fname, fsize, lastModifiedTimestamp);
            var cipher = EncyptHelper.Encrypt(algo, plain);

            return Path.Combine(_attachmentRootPath, cipher);
        }

        // 大文件上传之前, 判断是否是断点续传
        [HttpPost]
        public JsonResult BigFilePreCheck(string fname, long fsize, long lastModifiedTimestamp)
        {
            var chunkFolder = getBigFileChunkFolder(fname, fsize, lastModifiedTimestamp);

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

        /// <summary>
        /// 新增上传附件记录
        /// </summary>
        private void InsertAttachment(string filename, long filesize, string savepath)
        {
            var attachment = new InsertAttachmentInput
            {
                Name = filename,
                Size = filesize,
                SavePath = savepath
            };

            _attachmentService.Insert(attachment);
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

                InsertAttachment(file.FileName, long.Parse(Request["size"]), filepath);
            }
            else
            {
                var fname = Request["name"];                                                // 文件名称
                var chunk = Request["chunk"];                                               // 当前分片批次
                var fsize = long.Parse(Request["size"]);                                    // 文件大小
                var lastModifiedTimestamp = long.Parse(Request["lastModifiedDate"]);        // 文件最后修改时间

                var chunkFolder = getBigFileChunkFolder(fname, fsize, lastModifiedTimestamp);

                if (!Directory.Exists(chunkFolder))
                    Directory.CreateDirectory(chunkFolder);

                var filepath = Path.Combine(chunkFolder, chunk);

                file.SaveAs(filepath);
            }

            return AjaxOk();
        }

        [HttpPost]
        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <param name="size">文件大小</param>
        /// <param name="lastModifiedTimestamp">文件最后修改时间</param>
        /// <param name="chunks">合并文件总批次</param>
        public JsonResult MergeFile(string name, int size, long lastModifiedTimestamp, int chunks)
        {
            var chunkFolder = getBigFileChunkFolder(name, size, lastModifiedTimestamp);

            // 上传完成合并文件
            var fns = Directory.GetFiles(chunkFolder).OrderBy(d => Convert.ToInt32(d.Substring(d.LastIndexOf("\\") + 1)));
            var segCount = fns.Count();

            if (chunks == segCount)
            {
                var filepath = Path.Combine(chunkFolder, name);

                using (var fs = System.IO.File.Create(filepath))
                {
                    foreach (var fn in fns)
                    {
                        var segContent = System.IO.File.ReadAllBytes(fn);
                        fs.Write(segContent, 0, segContent.Length);

                        // 删除分片
                        System.IO.File.Delete(fn);
                    }

                    InsertAttachment(name, size, filepath);
                }
            }
            else
            {
                return AjaxFail("文件分片数不够");
            }

            return AjaxOk();
        }
    }
}