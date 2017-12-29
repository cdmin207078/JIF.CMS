using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services;
using JIF.CMS.Services.SysManager;
using JIF.CMS.Services.SysManager.Dtos;
using JIF.CMS.Web.Framework.Controllers;
using JIF.CMS.Web.Framework.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.CMS.Management.Controllers
{
    public class SysManagerController : AdminControllerBase
    {
        private readonly ISysManagerService _sysManagerService;

        public SysManagerController(ISysManagerService sysManagerService)
        {
            _sysManagerService = sysManagerService;
        }

        [HttpGet]
        public ActionResult Index(string Q = "", int pageIndex = JIFConstants.SYS_PAGE_INDEX, int pageSize = JIFConstants.SYS_PAGE_SIZE)
        {
            Q = Q.Trim();

            ViewBag.list = _sysManagerService.Get(Q, pageIndex, pageSize);

            ViewBag.Q = Q;

            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SysAdminInertBasicInfoInput model)
        {
            if (Request["Enable"] != null && Request["Enable"].ToString() == "on")
            {
                model.Enable = true;
            }

            _sysManagerService.Add(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var Manager = _sysManagerService.Get(id);

            if (Manager == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Manager = Manager;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBasicInfo(int id, SysAdminUpdateBasicInfoInput model)
        {
            if (Request["Enable"] != null && Request["Enable"].ToString() == "on")
            {
                model.Enable = true;
            }

            _sysManagerService.UpdateBasicInfo(id, model);

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePwd(int id, string newPwd)
        {
            _sysManagerService.UpdatePwd(id, newPwd);

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAvatar()
        {
            throw new NotImplementedException();
        }
    }
}