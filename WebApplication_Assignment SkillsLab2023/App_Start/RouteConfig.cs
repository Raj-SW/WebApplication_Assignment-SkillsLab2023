using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication_Assignment_SkillsLab2023
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "Home",
            //    url: "Home/Index/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Authentication", action = "LoginPage", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "RegisterPage",
                url: "Authentication/RegisterPage/{id}",
               defaults: new { controller = "Authentication", action = "RegisterPage", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "Index",
              url: "Home/Index/{id}",
             defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "LoginPage",
               url: "Authentication/LoginPage/{id}",
              defaults: new { controller = "Authentication", action = "LoginPage", id = UrlParameter.Optional }
          );
        }
    }
}
