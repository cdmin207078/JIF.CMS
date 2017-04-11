﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JIF.CMS.Management.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            cors.SupportsCredentials = true;

            config.EnableCors(cors);

            //config.EnableCors(new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true });
            //config.EnableCors();

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
