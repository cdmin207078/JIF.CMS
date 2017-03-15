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

        public ActionResult Index()
        {
            ViewBag.Managers = _sysManagerService.Load(pageIndex: 1, pageSize: 100);

            return View();
        }
    }
}