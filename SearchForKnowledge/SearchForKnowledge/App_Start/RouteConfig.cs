using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SearchForKnowledge
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new { controller = "First", action = "FirstView" });
            routes.MapRoute("Register", "register", new { Controller = "Users", action = "Register" });
            routes.MapRoute("About", "about", new { Controller = "About", action = "About" });
        }
    }
}
