﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homework7
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GIFTranslator",
                url: "{controller}/{action}/{lastWord}",
                defaults: new { controller = "Request", action = "TranslateGIF", lastWord = UrlParameter.Optional }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", lastWord = UrlParameter.Optional }
            );
        }
    }
}
