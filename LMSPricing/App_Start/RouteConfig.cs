using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMSPricing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "orderResult",
              url: "result/{v1}/{v2}/{v3}/{v4}",
              defaults: new { controller = "Home", action = "OrderResult", v1 = UrlParameter.Optional, v2 = UrlParameter.Optional, v3 = UrlParameter.Optional, v4 = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "order",
                url: "order/{packetID}",
                defaults: new { controller = "Home", action = "Order", packetID = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "agent",
              url: "agent/{title}",
              defaults: new { controller = "Home", action = "Agent", title = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
