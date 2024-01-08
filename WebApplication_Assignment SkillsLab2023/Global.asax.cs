using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication_Assignment_SkillsLab2023.ExceptionHandling;
using WebApplication_Assignment_SkillsLab2023.Logger;
using WebApplication_Assignment_SkillsLab2023.Logger.Interface;

namespace WebApplication_Assignment_SkillsLab2023
{
    public class MvcApplication : System.Web.HttpApplication
    { 
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
