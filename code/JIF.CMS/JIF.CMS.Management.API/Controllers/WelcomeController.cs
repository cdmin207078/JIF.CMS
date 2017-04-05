using JIF.CMS.Management.API.Models;
using JIF.CMS.Services.Authentication;
using JIF.CMS.Services.SysManager;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JIF.CMS.Management.API.Controllers
{
    [EnableCors("http://localhost:8888", "*", "*")]
    public class WelcomeController : ApiController
    {
        private readonly ISysManagerService _sysManagerService;
        private readonly IAuthenticationService _authenticationService;

        public WelcomeController(ISysManagerService sysManagerService,
            IAuthenticationService authenticationService)
        {
            _sysManagerService = sysManagerService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IHttpActionResult Login(LoginViewModel model)
        {
            var userInfo = _sysManagerService.Login(model.Account, model.Password);

            if (userInfo != null)
            {
                var sysAdmin = _sysManagerService.Get(userInfo.UserId);

                _authenticationService.SignIn(sysAdmin, true);

                return Ok("登陆成功");
            }

            return Ok("登陆失败");
        }
    }
}
