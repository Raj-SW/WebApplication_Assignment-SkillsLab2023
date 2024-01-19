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
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using WebApplication_Assignment_SkillsLab2023.App_Start;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
namespace WebApplication_Assignment_SkillsLab2023
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseActivator(new ContainerJobActivator(UnityConfig.Container))
                .UseSqlServerStorage("Server=localhost; Database=HangfireTest; Integrated Security=True; TrustServerCertificate=True;");
            yield return new BackgroundJobServer();
        }

        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HangfireAspNet.Use(GetHangfireServers);
            BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));

            //BackgroundJob.Schedule<IEnrolmentBL>(enrolmentBl => enrolmentBl.AutomaticEnrolmentProcessingForAllTrainingAsync(), TimeSpan.FromMinutes(1));
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
    }
}
