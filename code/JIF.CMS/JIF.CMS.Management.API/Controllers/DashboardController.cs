﻿using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using JIF.CMS.WebApi.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JIF.CMS.Management.API.Controllers
{
    public class DashboardController : AdminBaseController
    {
        [HttpGet]
        public IHttpActionResult GOO(string name)
        {
            return Ok(name);
        }

        [HttpPost]
        public IHttpActionResult POO(string name)
        {
            return Ok(name);
        }
    }
}
