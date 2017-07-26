using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JIF.CMS.Web.Framework.Controllers
{
    public abstract class BaseController : Controller
    {
        [NonAction]
        protected JsonResult AjaxOk()
        {
            return Json(new { success = true });
        }

        [NonAction]
        protected JsonResult AjaxOk(string message)
        {
            return Json(new { success = true, message = message });
        }

        [NonAction]
        protected JsonResult AjaxOk<T>(T data)
        {
            return Json(new { success = true, data = data });
        }

        [NonAction]
        protected JsonResult AjaxOk<T>(string message, T data)
        {
            return Json(new { success = true, message = message, data = data });
        }

        [NonAction]
        protected JsonResult AjaxFail()
        {
            return Json(new { success = false });
        }

        [NonAction]
        protected JsonResult AjaxFail(string message)
        {
            return Json(new { success = false, message = message });
        }

        [NonAction]
        protected JsonResult AjaxFail<T>(T data)
        {
            return Json(new { success = false, data = data });
        }

        [NonAction]
        protected JsonResult AjaxFail<T>(string message, T data)
        {
            return Json(new { success = false, message = message, data = data });
        }
    }
}
