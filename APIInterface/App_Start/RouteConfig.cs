using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using APIInterface.App_Start;

namespace APIInterface
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          
            routes.MapRoute(
            "OverRide", 
            "{*customRoute}",
            new { controller = "Rental", action = "Index", customRoute = UrlParameter.Optional },
            new { customRoute = new PermaLinkRouteConstraint() });



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
