using System.Web;
using System.Web.Optimization;

namespace XChess
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

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            // loginpage
            bundles.Add(new StyleBundle("~/Content/LoginPage/css").Include(
                           "~/Content/bootstrap.css",
                             "~/Content/LoginPage.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/XChessStyle.css"));
            bundles.Add(new StyleBundle("~/Content/play/css").Include("~/Content/BanCo.css"));

            bundles.Add(new ScriptBundle("~/assets/appjs").Include(
                       "~/Scripts/Common.js",
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Chessjs").Include(
                      "~/Scripts/Chess.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ChessBoardjs").Include(
                      "~/Scripts/ChessBoard.js"));

        }
    }
}
