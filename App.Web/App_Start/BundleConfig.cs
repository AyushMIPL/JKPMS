using System.Web;
using System.Web.Optimization;

namespace App.Web
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
      //            "~/Scripts/jquery-{version}.js"));

      //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
      //            "~/Scripts/jquery.validate*"));

      //// Use the development version of Modernizr to develop with and learn from. Then, when you're
      //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
      //            "~/Scripts/modernizr-*"));

      //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
      //          "~/Scripts/bootstrap.js",
      //          "~/Scripts/respond.js"));

      //bundles.Add(new StyleBundle("~/Content/css").Include(
      //          "~/Content/bootstrap.css",
      //          "~/Content/site.css"));
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js",
                  //"~/Scripts/jquery-1.11.1.min.js",       // Code Line Old Jquery Version Commit By Himanshu Rajput
        //"~/Scripts/jquery.dataTables.min.js",
                  "~/Scripts/jquery-ui.js",
                  "~/Content/endless/bootstrap/js/bootstrap.min.js",
                  "~/Content/Toster/toastr.js",
                  "~/Content/endless/js/jquery.dataTables.min.js",
                  "~/Content/endless/js/modernizr.min.js",
                  "~/Content/endless/js/pace.min.js",
                  "~/Content/endless/js/jquery.popupoverlay.min.js",
                  "~/Content/endless/js/jquery.slimscroll.min.js",
                  "~/Content/endless/js/jquery.cookie.min.js",
                  "~/Content/endless/js/endless/endless.js",
                  "~/Content/endless/datePicker/js/bootstrap-datepicker.js",
        //"~/Content/endless/datePicker/css/datepicker.js",
                  "~/Scripts/jquery.dataTables.rowGrouping.js",
                  "~/Scripts/jquery.dataTables.columnFilter.js",
                  "~/Content/Select2/select2.min.js",
                  "~/Content/endless/js/jquery.maskedinput.min.js",
                  "~/Scripts/autoNumeric.js",
                  "~/Content/endless/js/chart.js",
                  "~/Content/endless/js/jquery-ui.js",
                    "~/Scripts/Date.Validate.js"
                  ));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.validate*",
                  "~/Scripts/jquery.unobtrusive-ajax*"
                  ));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/endless/css/font-awesome.min.css",
                "~/Content/endless/css/jquery.dataTables_themeroller.css",
                "~/Content/endless/css/pace.css",
                "~/Content/endless/css/endless.min.css",
                "~/Content/Toster/toastr.css",
                "~/Content/endless/css/endless-skin.css",
                "~/Content/toastr.css",
                "~/Content/dataTables.bootstrap.css",
                "~/Content/Select2/select2.min.css",
                "~/Content/site.css"));
      //BundleTable.EnableOptimizations = true;
    }
  }
}
