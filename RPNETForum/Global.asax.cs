using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Elmah;

namespace RPNETForum {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (Directory.Exists(HostingEnvironment.MapPath("~/App_Data/ImgCache"))) {
                var dir = new DirectoryInfo(HostingEnvironment.MapPath("~/App_Data/ImgCache"));

                foreach (var file in dir.GetFiles()) {
                    file.Delete();
                }
            }
        }

        void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e) {
            if (e.Exception.GetBaseException() is HttpRequestValidationException) {
                e.Dismiss();
            }
        }
    }
}
