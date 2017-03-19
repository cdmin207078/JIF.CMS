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
            bundles.Add(new StyleBundle("~/styles/adminlte")
                .Include("~/Content/AdminLTE/bootstrap/bootstrap.css")
                .Include("~/Content/AdminLTE/dist/css/AdminLTE.css")
                .Include("~/Content/AdminLTE/dist/css/skins/_all-skins.css")
                .Include("~/Content/AdminLTE/plugins/iCheck/all.css")
                .Include("~/Content/jquery-confirm/jquery-confirm.min.css")
                .Include("~/Content/ios-switch/switch.css")
                .Include("~/Content/AdminLTE/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/scripts/adminlte").Include(
                "~/Content/AdminLTE/plugins/jQuery/jquery-2.2.3.min.js",
                "~/Content/AdminLTE/bootstrap/bootstrap.js",
                "~/Content/AdminLTE/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/Content/AdminLTE/dist/js/app.js",
                "~/Content/AdminLTE/plugins/iCheck/icheck.min.js",
                "~/Content/jquery-confirm/jquery-confirm.min.js",
                "~/Content/ios-switch/switch.js",
                "~/Scripts/Layout.js"
                ));
        }
    }
}