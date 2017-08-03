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
                "~/Content/AdminLTE/bootstrap/bootstrap.css",
                "~/Content/AdminLTE/dist/css/AdminLTE.css",
                "~/Content/AdminLTE/dist/css/skins/_all-skins.css",
                "~/Content/AdminLTE/plugins/iCheck/all.css",
                "~/Content/ios-switch/switch.css",
                "~/Content/jquery-confirm/jquery-confirm.css")
                .Include("~/Content/AdminLTE/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/scripts/adminlte").Include(
                "~/Content/AdminLTE/plugins/jQuery/jquery-2.2.3.min.js",
                "~/Content/AdminLTE/bootstrap/bootstrap.js",
                "~/Content/AdminLTE/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/Content/AdminLTE/plugins/iCheck/icheck.js",
                "~/Content/AdminLTE/dist/js/app.js",
                "~/Content/moment/moment.js",
                "~/Content/lodash/lodash.js",
                "~/Content/ios-switch/switch.js",
                "~/Content/jquery-confirm/jquery-confirm.js",
                "~/Scripts/Layout.js"
                ));

            // bootstrap-daterangepicker
            bundles.Add(new StyleBundle("~/styles/datetimepicker").Include("~/Content/datetimepicker/jquery.datetimepicker.min.css"));
            bundles.Add(new ScriptBundle("~/scripts/datetimepicker").Include("~/Content/datetimepicker/jquery.datetimepicker.full.js"));

            // editor.md
            bundles.Add(new StyleBundle("~/styles/editor.md").Include("~/Content/editor.md/css/editormd.css"));
            bundles.Add(new ScriptBundle("~/scripts/editor.md").Include("~/Content/editor.md/editormd.js"));


            // editor.md
            bundles.Add(new StyleBundle("~/styles/tagsinput").Include("~/Content/jquery-tagsinput/jquery.tagsinput.css"));
            bundles.Add(new ScriptBundle("~/scripts/tagsinput").Include("~/Content/jquery-tagsinput/jquery.tagsinput.js"));

            // web uploader
            bundles.Add(new StyleBundle("~/styles/web-uploader").Include("~/Content/webuploader/webuploader.css"));
            bundles.Add(new ScriptBundle("~/scripts/web-uploader").Include("~/Content/webuploader/webuploader.js"));

            // jquery ui
            bundles.Add(new StyleBundle("~/styles/jquery-ui").Include("~/Content/jquery-ui/jquery-ui.css"));
            bundles.Add(new ScriptBundle("~/scripts/jquery-ui").Include("~/Content/jquery-ui/jquery-ui.js"));


            // pages
            bundles.Add(new ScriptBundle("~/scripts/page/article").Include("~/scripts/pages/article.js"));
            bundles.Add(new ScriptBundle("~/scripts/page/article-categories").Include("~/scripts/pages/article-categories.js"));
            bundles.Add(new ScriptBundle("~/scripts/page/attachment").Include("~/scripts/pages/attachment.js"));
        }
    }
}