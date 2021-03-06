﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyInstagram.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default1",
            //    url: "{username}",
            //    defaults: new { controller = "Home", action = "Index", username = UrlParameter.Optional }
            //);

           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{username}",
                defaults: new { controller = "User", action = "Page", username = UrlParameter.Optional }
            );



        }
    }
}
