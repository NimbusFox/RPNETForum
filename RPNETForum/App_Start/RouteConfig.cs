using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RPNETForum {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new {controller = "Home", action = "Logout"}
            );

            routes.MapRoute(
                name: "Profile",
                url: "User/{id}",
                defaults: new {controller = "Profile", action = "Index", id = ""}
            );

            routes.MapRoute(
                name: "Verify",
                url: "Verify/{token}",
                defaults: new { controller = "Home", action = "Verify", token = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Forum",
                url: "Forum/{id}",
                defaults: new {controller = "Forum", action = "Index", id = -1}
            );

            routes.MapRoute(
                name: "Thread",
                url: "Thread/{id}",
                defaults: new {controller = "Thread", action = "Index", id = -1}
            );

            routes.MapRoute(
                name: "Avatar",
                url: "Avatar/{id}",
                defaults: new {controller = "Home", action = "Avatar", id = -1}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
