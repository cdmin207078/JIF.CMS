using JIF.CMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace JIF.CMS.Management.API.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthenticationAttribute : AuthorizeAttribute
    {
        public IWorkContext WorkContext { get; set; }

        private readonly bool _dontValidate;

        public AdminAuthenticationAttribute()
            : this(false)
        {

        }

        public AdminAuthenticationAttribute(bool dontValidate)
        {
            this._dontValidate = dontValidate;
        }

        public override void OnAuthorization(HttpActionContext context)
        {
            if (_dontValidate)
                return;

            if (context == null)
            {
                throw new ArgumentNullException("Authen Context is Null.");
            }

            if (!this.HasAdminAccess(context))
            {
                this.HandleUnauthorizedRequest(context);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext context)
        {
            var response = new HttpResponseMessage();

            response.StatusCode = HttpStatusCode.MethodNotAllowed;
            response.Content = new StringContent(JsonConvert.SerializeObject(new
            {
                success = false,
                code = 405,
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

            if (WorkContext == null || WorkContext.CurrentUser == null)
                return false;
            else
                return true;
        }
    }
}