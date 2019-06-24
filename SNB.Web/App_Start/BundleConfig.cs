using System.Web;
using System.Web.Optimization;

namespace SNB.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //--------------------CSS--------------------

            // Theme Global CSS---
            bundles.Add(new StyleBundle("~/Content/theme-global").Include(
                      "~/Plugins/global_assets/css/icons/icomoon/styles.min.css",
                      "~/Plugins/assets/css/bootstrap.min.css",
                      "~/Plugins/assets/css/bootstrap_limitless.min.css",
                      "~/Plugins/assets/css/layout.min.css",
                      "~/Plugins/assets/css/components.min.css",
                      "~/Plugins/assets/css/colors.min.css"));

            // Custom CSS---
            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/site.css"));


            //------------------Scripts------------------

            // Theme Core Scripts---
            bundles.Add(new ScriptBundle("~/bundles/theme-core").Include(
                      "~/Plugins/global_assets/js/main/jquery.min.js",
                      "~/Plugins/global_assets/js/main/bootstrap.bundle.min.js",
                      "~/Plugins/global_assets/js/plugins/loaders/blockui.min.js",
                      "~/Plugins/global_assets/js/plugins/ui/slinky.min.js"));

            // Theme App Script---
            bundles.Add(new ScriptBundle("~/bundles/theme-app").Include(
                      "~/Plugins/assets/js/app.js"));

            // Dashboard Script---
            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                      "~/Plugins/global_assets/js/demo_pages/dashboard.js"));

            // Sticky Script---
            bundles.Add(new ScriptBundle("~/bundles/sticky").Include(
                      "~/Plugins/global_assets/js/plugins/ui/sticky.min.js"));

            // NavBar Sticky Script---
            bundles.Add(new ScriptBundle("~/bundles/navbar-sticky").Include(
                      "~/Plugins/global_assets/js/demo_pages/navbar_multiple_sticky.js"));

            // Common Script---
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                      "~/Scripts/common.js"));

        }
    }
}
