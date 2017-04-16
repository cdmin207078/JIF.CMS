using JIF.CMS.WebApi.Framework.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.WebApi.Framework.Controllers
{
    [AdminAuthorize]
    public class AdminBaseController : BaseController
    {

    }
}
