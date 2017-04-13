using JIF.CMS.Management.API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JIF.CMS.Management.API.Controllers
{
    [AdminAuthentication]
    public class AdminBaseController : BaseController
    {
    }
}
