using System.Web;
using System.Web.Mvc;

namespace WebApplication_Assignment_SkillsLab2023
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
