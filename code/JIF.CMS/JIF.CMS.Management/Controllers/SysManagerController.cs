using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services.SysManager;
using JIF.CMS.Services.SysManager.Dtos;
using JIF.CMS.Web.Framework.Controllers;
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
        public ActionResult Index(string s = "", int pageIndex = JIFConsts.SYS_PAGE_INDEX, int pageSize = JIFConsts.SYS_PAGE_SIZE)
        {
            s = s.Trim();

            ViewBag.Managers = _sysManagerService.Load(s, pageIndex, pageSize);

            ViewBag.SearchWords = s;

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
        public ActionResult UpdatePwd(int id, string newPwd)
        {
            _sysManagerService.UpdatePwd(id, newPwd);

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        public ActionResult UpdateAvatar()
        {
            throw new NotImplementedException();
        }






        [HttpPost]
        public ActionResult DDD(List<DDDC> model)
        {
            return Json(model);
        }
    }




    public class DDDC
    {
        public int Id { get; set; }

        public List<DDDKV> KV { get; set; }
    }


    public class DDDKV
    {
        public string k { get; set; }

        public string v { get; set; }
    }
}