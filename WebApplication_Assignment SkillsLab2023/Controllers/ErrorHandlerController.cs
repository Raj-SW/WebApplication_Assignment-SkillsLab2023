using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class ErrorHandlerController : Controller
    {
        // GET: ErrorHandler
        public ActionResult InternalServerError()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult AuthorizationError()
        {
            return View();
        }
        public ActionResult SessionTimeOut()
        {
            return View();
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}