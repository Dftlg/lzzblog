using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lzz博客
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Items",
                url: "Items/{id}",
                defaults: new { controller = "CommonModel", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}/{page}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional },
               namespaces: new string[] { "lzz博客.Controllers" }
               );
            routes.MapRoute(
                name: "Manage",
                url: "{controller}/{action}/{id}/{page}",
                defaults: new { controller = "Category", action = "Manage", id = UrlParameter.Optional, page = UrlParameter.Optional },
                namespaces:new string[] { "lzz博客.Controllers"}
                );
        }
    }
}
