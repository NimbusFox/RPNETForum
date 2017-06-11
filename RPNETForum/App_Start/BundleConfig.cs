using System.Web;
using System.Web.Optimization;

namespace RPNETForum {
    public class BundleConfig {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/js/dependencies").IncludeDirectory("~/Scripts/", "*.js"));
            bundles.Add(new ScriptBundle("~/js/controls").IncludeDirectory("~/Scripts/RPNETForum/controls", "*.js"));

            bundles.Add(new StyleBundle("~/css/site").IncludeDirectory("~/Content/base", "*.css"));
        }
    }
}
