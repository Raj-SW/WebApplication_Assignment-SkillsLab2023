using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication_Assignment_SkillsLab2023.Custom
{
    public class CustomAuthorizationAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public string[] AuthorizationRoles { get; set; }
        public CustomAuthorizationAttribute(string roles)
        {
            this.Roles = roles;
            AuthorizationRoles = this.Roles.Split(',');
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var dfController = filterContext.Controller as Controller;
            if (dfController != null && dfController.Session["CurrentRole"] != null)
            {
                var currentRole = dfController.Session["CurrentRole"] as string;
                if (!AuthorizationRoles.Contains(currentRole))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Common", action = "AccesDenied" }));
                }
            }
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Common", action = "AccesDenied" }));

        }

    }
}