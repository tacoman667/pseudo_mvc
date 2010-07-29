using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace PseudoMvc {
    public static class RouteCollectionExtensions {

        private static void Map(this RouteCollection routes, string url, string newRoute) {

            var route = new Route(
                url,
                new WebFormRouteHandler()
                );

            routes.Add(route);

        }

        public static void IgnoreRoute(this RouteCollection routes, string url) {
            IgnoreRoute(routes, url, null);
        }

        public static void IgnoreRoute(this RouteCollection routes, string url, object defaults) {

            routes.Add(new Route(url, new StopRoutingHandler()));

        }

        public static void Map(this RouteCollection routes, string name, string url, object defaults) {

            string routeUrl = String.Empty;
            string newUrl = String.Empty;

            var controllerName = defaults.GetPropertyValue("controller") as string;
            var actionName = defaults.GetPropertyValue("action") as string;
            var id = defaults.GetPropertyValue("id") as string;

            routes.Add(name,
                       new Route(url,
                                 new RouteValueDictionary() { { "controller", controllerName }, { "action", actionName }, { "id", id } },
                                 new WebFormRouteHandler())
                      );
            
        }

    }
}
