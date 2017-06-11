using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        }

        void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e) {
            if (e.Exception.GetBaseException() is HttpRequestValidationException) {
                e.Dismiss();
            }
        }
    }
}
