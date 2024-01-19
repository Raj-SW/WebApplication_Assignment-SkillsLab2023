using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication_Assignment_SkillsLab2023.SessionManagement
{
    public class UserSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ( filterContext.HttpContext.Session["CurrentRole"] == null
                || filterContext.HttpContext.Session["CurrentRoleId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/LoginPage");
            }
        }
    }
}