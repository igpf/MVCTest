using System.Web;
using System.Web.Optimization;

namespace MVCTest
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            //css files
            bundles.Add(new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/bootstrap.css")
                .Include("~/Content/css/bootstrap-grid.css")
                .Include("~/Content/css/bootstrap-utilities.css")
                .Include("~/Content/css/Chart.css")
                .Include("~/Content/css/sb-admin-2.css")
                .Include("~/Content/css/fontawesome.css")
                .Include("~/Content/css/fullcalendar.css")
                .Include("~/Content/css/Site.css")
            );

            //scripts
            bundles.Add(new Bundle("~/Bundles/js")
                .Include("~/Content/js/jquery-3.4.1.js")
                .Include("~/Content/js/jquery-easing.js")
                .Include("~/Content/js/modernizr-*")
                .Include("~/Content/js/moment.js")
                .Include("~/Content/js/fontawesome.js")
                .Include("~/Content/js/jquery.validate*")
                .Include("~/Content/js/bootbox.js")
                .Include("~/Content/js/Chart.js")
                .Include("~/Content/js/fullcalendar.js")
                .Include("~/Content/js/gcal.js")
                .Include("~/Content/js/sb-admin-2.js")
                .Include("~/Content/js/bootstrap.js")
                
            );


        }
    }
}
