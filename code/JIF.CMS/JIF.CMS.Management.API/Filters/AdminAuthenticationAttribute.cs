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

namespace JIF.CMS.Management.API.Filters
{
    public class AdminAuthenticationAttribute : IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}