using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace JIF.CMS.Management
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            // AdminLte
            bundles.Add(new StyleBundle("~/styles/adminlte").Include(
                "~/Content/AdminLTE/bootstrap/css/bootstrap.css",
                "~/Content/AdminLTE/dist/css/AdminLTE.css",
                "~/Content/AdminLTE/dist/css/skins/_all-skins.min.css",
                "~/Content/AdminLTE/plugins/iCheck/all.css",
                "~/Content/jquery-confirm/jquery-confirm.min.css",
                "~/Content/ios-switch/switch.css")
                .Include("~/Content/AdminLTE/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/scripts/adminlte").Include(
                "~/Content/AdminLTE/plugins/jQuery/jquery-2.2.3.min.js",
                "~/Content/AdminLTE/bootstrap/js/bootstrap.min.js",
                "~/Content/AdminLTE/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/Content/AdminLTE/dist/js/app.min.js",
                "~/Content/AdminLTE/plugins/iCheck/icheck.min.js",
                "~/Content/jquery-confirm/jquery-confirm.min.js",
                "~/Content/ios-switch/switch.js"
                ));


            // layout js
            bundles.Add(new ScriptBundle("~/scripts/layout").Include(
                        "~/Scripts/Layout.js"
                        ));

        }

    }
}