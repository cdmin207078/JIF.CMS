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
        // GET: Attachment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var file = Request.Files[0];

            var filepath = Path.Combine(Server.MapPath("../attachments"), file.FileName);
            file.SaveAs(filepath);

            return Json("OK");
        }
    }
}