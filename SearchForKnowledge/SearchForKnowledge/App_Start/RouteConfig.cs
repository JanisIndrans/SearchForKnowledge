﻿using System;
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

            routes.MapRoute("Home", "", new { Controller = "Posts", action = "Index" });
            routes.MapRoute("Register", "register", new { Controller = "Users", action = "Register" });
            routes.MapRoute("About", "about", new { Controller = "About", action = "About" });
            routes.MapRoute("Login", "login", new { Controller = "Users", action = "Login" });
            routes.MapRoute("Logout", "logout", new {controller = "Users", action = "Logout"});
            routes.MapRoute("CreatePost", "createpost", new { controller = "Posts", action = "CreatePost" });
            routes.MapRoute("AdminPage", "adminpage", new { controller = "Users", action = "AdminPage" });
            routes.MapRoute("WelcomePage", "welcome", new {controller = "Users", action = "WelcomePage"});
            routes.MapRoute("Search", "search", new { controller = "Posts", action = "SearchPosts"});
            routes.MapRoute("Selection", "categories", new { controller = "Posts", action = "CategoryPosts" });
            routes.MapRoute("UserPosts", "user", new {controller = "Posts", action = "UserPosts"});


        }
    }
}
