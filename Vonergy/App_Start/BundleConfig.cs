using System.Web;
using System.Web.Optimization;

namespace Vonergy
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

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                        "~/Scripts/Chart.js*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/DashBoard.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/genericFunction.js",
                      "~/Scripts/funcionario.js",
                       "~/Scripts/empresa.js",
                      "~/Scripts/login.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                         "~/Content/bootstrap.css",
                         "~/Content/LoginStyle.css",
                          "~/Content/Funcionario.css",
                         "~/Content/Empresa.css",
                         "~/Content/DashBoard.css"));


            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
            //~/Scripts/Inputmask/dependencyLibs/inputmask.dependencyLib.js",  //if not using jquery
            "~/Scripts/Inputmask/inputmask.js",
            "~/Scripts/Inputmask/jquery.inputmask.js",
            "~/Scripts/Inputmask/inputmask.extensions.js",
            "~/Scripts/Inputmask/inputmask.date.extensions.js",
            //and other extensions you want to include
            "~/Scripts/Inputmask/inputmask.numeric.extensions.js"));
        }
    }
}
