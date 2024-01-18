using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.Logger.Interface;

namespace WebApplication_Assignment_SkillsLab2023.ExceptionHandling
{
    public class CustomGlobalExceptionHandling : HandleErrorAttribute
    {
        private readonly ILogger _logger;
        public CustomGlobalExceptionHandling(ILogger logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            _logger.LogError(filterContext.Exception);
            filterContext.ExceptionHandled = true;
            //filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                TempData = filterContext.Controller.TempData
            };
        }
    }
}