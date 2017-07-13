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
                "~/Content/lodash/lodash.js",
                "~/Scripts/Layout.js"
                ));

            // bootstrap-datepicker
            bundles.Add(new StyleBundle("~/styles/bootstrap-datepicker").Include("~/Content/bootstrap-datepicker/css/bootstrap-datepicker.css"));
            bundles.Add(new ScriptBundle("~/scripts/bootstrap-datepicker").Include(
                "~/Content/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/Content/bootstrap-datepicker/locales/bootstrap-datepicker.zh-CN.min.js"));

            // editor.md
            bundles.Add(new StyleBundle("~/styles/editor.md").Include("~/Content/editor.md/css/editormd.css"));
            bundles.Add(new ScriptBundle("~/scripts/editor.md").Include("~/Content/editor.md/editormd.js"));

            // web uploader
            bundles.Add(new StyleBundle("~/styles/web-uploader").Include("~/Content/webuploader/webuploader.css"));
            bundles.Add(new ScriptBundle("~/scripts/web-uploader").Include("~/Content/webuploader/webuploader.js"));

            // pages
            bundles.Add(new ScriptBundle("~/scripts/page/article").Include("~/scripts/pages/article.js"));
            bundles.Add(new ScriptBundle("~/scripts/page/attachment").Include("~/scripts/pages/attachment.js"));


        }
    }
}