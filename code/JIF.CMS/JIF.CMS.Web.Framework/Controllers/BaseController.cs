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
        protected JsonResult JsonOk()
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected JsonResult JsonOk(string message)
        {
            return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected JsonResult JsonOk<T>(T data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected JsonResult JsonOk<T>(string message, T data)
        {
            return Json(new { success = true, message = message, data = data }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected JsonResult JsonFail()
        {
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected JsonResult JsonFail(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected JsonResult JsonFail<T>(T data)
        {
            return Json(new { success = false, data = data }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected JsonResult JsonFail<T>(string message, T data)
        {
            return Json(new { success = false, message = message, data = data }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 禁用session 后, TempData无法使用, 须重写基类 CreateTempDataProvider方法, 自定义TempData 的提供程序
        /// </summary>
        /// <returns></returns>
        protected override ITempDataProvider CreateTempDataProvider()
        {
            // http://afana.me/archive/2010/11/23/session-less-controllers-and-tempdata-aspnet-mvc.aspx/
            return new CookieTempDataProvider(HttpContext);
            //return base.CreateTempDataProvider();
        }
    }
}
