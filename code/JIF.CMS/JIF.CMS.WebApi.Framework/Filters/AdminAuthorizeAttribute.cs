using JIF.CMS.Core;
using JIF.CMS.Core.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JIF.CMS.WebApi.Framework.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _dontValidate;

        public AdminAuthorizeAttribute()
            : this(false)
        {

        }

        public AdminAuthorizeAttribute(bool dontValidate)
        {
            this._dontValidate = dontValidate;
        }

        public override void OnAuthorization(HttpActionContext context)
        {
            if (_dontValidate)
                return;

            if (!this.HasAdminAccess(context))
            {
                this.HandleUnauthorizedRequest(context);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext context)
        {
            var response = new HttpResponseMessage();

            response.StatusCode = HttpStatusCode.Unauthorized;
            response.Content = new StringContent(JsonConvert.SerializeObject(new
            {
                success = false,
                code = 401,
                message = "无权访问"
            }), Encoding.UTF8, "application/json");

            context.Response = response;
        }

        public virtual bool HasAdminAccess(HttpActionContext context)
        {
            //授权权限判断
            //var permissionService = EngineContext.Current.Resolve<IPermissionService>();
            //bool result = permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel);
            //return result;

            var workContext = EngineContext.Current.Resolve<IWorkContext>();

            if (workContext == null || workContext.CurrentUser == null)
                return false;
            else
                return true;
        }
    }
}
