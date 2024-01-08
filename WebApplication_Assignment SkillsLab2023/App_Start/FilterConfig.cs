using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.ExceptionHandling;
using WebApplication_Assignment_SkillsLab2023.Logger.Interface;

namespace WebApplication_Assignment_SkillsLab2023
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            var logger = DependencyResolver.Current.GetService<ILogger>();
            filters.Add(new CustomGlobalExceptionHandling(logger));
        }
    }
}
