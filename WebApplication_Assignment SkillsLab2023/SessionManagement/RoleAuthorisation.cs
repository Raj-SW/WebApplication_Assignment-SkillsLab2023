using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication_Assignment_SkillsLab2023.SessionManagement
{
    public class RoleAuthorisation : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public string[] AuthorizedRoles { get; set; }
        public RoleAuthorisation(string roles)
        {
            this.Roles = roles;
            AuthorizedRoles = this.Roles.Split(',');
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRole = "";
            var dfController = filterContext.Controller as Controller;
            if (dfController != null && dfController.Session["CurrentRole"] != null)
            {
                 currentRole= dfController.Session["CurrentRole"] as string;
                if (!AuthorizedRoles.Contains(currentRole))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "ErrorHandler", action = "AuthorizationError" }));
                }
            }
            else 
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = $"{currentRole}View" }));
            }
        }
    }
}