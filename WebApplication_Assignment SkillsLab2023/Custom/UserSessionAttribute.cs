using System.Web.Mvc;
using Unity.Injection;

namespace WebApplication_Assignment_SkillsLab2023.Custom
{
    public class UserSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["CurentUser"]==null || filterContext.HttpContext.Session["CurrentRole"]==null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/LoginPage");
            }
        }
    }
}