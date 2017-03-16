using JIF.CMS.Core;
using JIF.CMS.Core.Domain;
using JIF.CMS.Services.SysManager;
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

        public ActionResult Index(string s = "", int pageIndex = JIFConsts.SYS_PAGE_INDEX,
            int pageSize = JIFConsts.SYS_PAGE_SIZE)
        {
            ViewBag.Managers = _sysManagerService.Load(s, pageIndex, pageSize);

            ViewBag.SearchKey = s;

            return View();
        }
    }
}