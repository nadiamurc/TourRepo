using System.Web;
using System.Web.Optimization;

namespace Tour
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Two places for JS files some specific for theme
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/Content/assets/web/assets/jquery/jquery.min.js",
                        "~/Content/assets/popper/popper.min.js",
                        "~/Content/assets/tether/tether.min.js",
                        "~/Content/assets/bootstrap/js/bootstrap.min.js",
                        "~/Content/assets/smoothscroll/smooth-scroll.js",
                        //"~/Content/assets/parallax/jarallax.min.js",
                        
                        "~/Content/assets/ytplayer/jquery.mb.ytplayer.min.js",
                        "~/Content/assets/vimeoplayer/jquery.mb.vimeo_player.js",
                        //"~/Content/assets/bootstrapcarouselswipe/bootstrap-carousel-swipe.js",
                        "~/Content/assets/touchswipe/jquery.touch-swipe.min.js",
                        "~/Content/assets/dropdown/js/script.min.js",
                        "~/Content/assets/masonry/masonry.pkgd.min.js",
                        "~/Content/assets/imagesloaded/imagesloaded.pkgd.min.js",
                        //"~/Content/assets/theme/js/script.js",
                        "~/Content/assets/slidervideo/script.js",
                        "~/Content/assets/gallery/player.min.js"
                        //"~/Content/assets/gallery/script.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        //"~/Scripts/modernizr-*"
                        ));

            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/assets/bootstrap/js/bootstrap.min.js"
                      //"~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/assets/web/assets/mobirise-icons/mobirise-icons.css",
                      "~/Content/assets/tether/tether.min.css",
                      "~/Content/assets/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/bootstrap/css/bootstrap-grid.min.css",
                      "~/Content/assets/bootstrap/css/bootstrap-reboot.min.css",
                      "~/Content/assets/socicon/css/styles.css",
                      "~/Content/assets/dropdown/css/style.css",
                      "~/Content/assets/theme/css/style.css",
                      "~/Content/assets/gallery/style.css",
                      "~/Content/assets/mobirise/css/mbr-additional.css"));
        }
    }
}
