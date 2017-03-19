using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services.SysManager;
using JIF.CMS.Services.SysManager.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIF.CMS.Management.Controllers
{
    public class SysManagerController : Controller
    {
        private readonly ISysManagerService _sysManagerService;

        public SysManagerController(ISysManagerService sysManagerService)
        {
            _sysManagerService = sysManagerService;
        }

        [HttpGet]
        public ActionResult Index(string s = "", int pageIndex = JIFConsts.SYS_PAGE_INDEX, int pageSize = JIFConsts.SYS_PAGE_SIZE)
        {
            ViewBag.Managers = _sysManagerService.Load(s, pageIndex, pageSize);

            ViewBag.SearchKey = s;

            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(SysAdminInertBasicInfo model)
        {
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
        public ActionResult UpdateBasicInfo(int id, SysAdminUpdateBasicInfo model)
        {
            if (Request["Enable"] != null && Request["Enable"].ToString() == "on")
            {
                model.Enable = true;
            }

            _sysManagerService.UpdateBasicInfo(id, model);

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        public ActionResult UpdatePwd(int id, string originalPwd, string newPwd)
        {
            _sysManagerService.UpdatePwd(originalPwd, newPwd);

            return RedirectToAction("update", new { id = id });
        }

        [HttpPost]
        public ActionResult UpdateAvatar()
        {
            throw new NotImplementedException();
        }
    }
}