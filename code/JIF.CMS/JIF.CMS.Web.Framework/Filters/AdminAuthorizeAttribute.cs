using JIF.CMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JIF.CMS.Web.Framework.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public IWorkContext WorkContext { get; set; }

        private readonly bool _dontValidate;

        public AdminAuthorizeAttribute()
            : this(false)
        {
        }

        public AdminAuthorizeAttribute(bool dontValidate)
        {
            this._dontValidate = dontValidate;
        }

        private bool HasAdminAccess(AuthorizationContext filterContext)
        {
            if (WorkContext == null || WorkContext.CurrentUser == null)
                return false;

            //授权权限判断
            //var permissionService = EngineContext.Current.Resolve<IPermissionService>();
            //bool result = permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel);
            //return result;
            return true;
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // 返回认证失败
            //filterContext.Result = new HttpUnauthorizedResult();

            // 跳转登陆地址
            var returnUrl = filterContext.RequestContext.HttpContext.Request.Url.PathAndQuery;

            filterContext.Result = new RedirectResult("/welcome?returnUrl=" + returnUrl);
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (_dontValidate)
                return;

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                throw new InvalidOperationException("You cannot use [AdminAuthorize] attribute when a child action cache is active");
            }

            if (!HasAdminAccess(filterContext))
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
