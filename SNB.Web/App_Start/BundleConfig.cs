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

            // Login Script---
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Plugins/global_assets/js/demo_pages/login.js"));

            // Uniform Script---
            bundles.Add(new ScriptBundle("~/bundles/uniform").Include(
                      "~/Plugins/global_assets/js/plugins/forms/styling/uniform.min.js"));

            // Sticky Script---
            //bundles.Add(new ScriptBundle("~/bundles/sticky").Include(
            //          "~/Plugins/global_assets/js/plugins/ui/sticky.min.js"));

            // NavBar Sticky Script---
            bundles.Add(new ScriptBundle("~/bundles/navbar-sticky").Include(
                      "~/Plugins/global_assets/js/plugins/ui/sticky.min.js",
                      "~/Plugins/global_assets/js/demo_pages/navbar_multiple_sticky.js"));

            // Bootstrap Date Time Scripts---
            bundles.Add(new ScriptBundle("~/bundles/daterange-picker").Include(
                "~/Plugins/global_assets/js/plugins/ui/moment/moment.min.js",
                "~/Plugins/global_assets/js/plugins/pickers/daterangepicker.js",
                "~/Plugins/global_assets/js/plugins/pickers/pickadate/picker.js",
                "~/Plugins/global_assets/js/plugins/pickers/pickadate/picker.date.js",
                "~/Plugins/global_assets/js/plugins/pickers/pickadate/picker.time.js",
                "~/Scripts/custom-date.js"));

            // Bootstrap File Scripts---
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-file").Include(
                      "~/Plugins/global_assets/js/plugins/uploaders/fileinput/fileinput.min.js",
                      "~/Plugins/global_assets/js/demo_pages/uploader_bootstrap.js",
                      "~/Scripts/custom-file.js"));

            // Sweet Alert Scripts---
            bundles.Add(new ScriptBundle("~/bundles/sweet-alert").Include(
                "~/Plugins/global_assets/js/plugins/notifications/sweet_alert.min.js",
                "~/Plugins/global_assets/js/demo_pages/extra_sweetalert.js"));

            // Ajax Call Script---
            bundles.Add(new ScriptBundle("~/bundles/ajax-call").Include(
                "~/Scripts/ajax-call.js"));

            // Ajax Call Script---
            bundles.Add(new ScriptBundle("~/bundles/user-profile").Include(
                "~/Plugins/global_assets/js/demo_pages/user_pages_profile_tabbed.js"));

            // Ajax Call Script---
            bundles.Add(new ScriptBundle("~/bundles/media-gallery").Include(
                "~/Plugins/global_assets/js/plugins/media/fancybox.min.js",
                "~/Plugins/global_assets/js/demo_pages/gallery_library.js"));

            // Ajax Call Script---
            bundles.Add(new ScriptBundle("~/bundles/table-responsive").Include(
                "~/Plugins/global_assets/js/plugins/tables/footable/footable.min.js",
                "~/Plugins/global_assets/js/demo_pages/table_responsive.js"));

            // Common Script---
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                      "~/Scripts/common.js"));

            // Common Script---
            bundles.Add(new ScriptBundle("~/bundles/property").Include(
                      "~/Scripts/Projects/Property.js"));

        }
    }
}
