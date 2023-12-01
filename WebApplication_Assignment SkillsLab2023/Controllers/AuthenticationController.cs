using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationBL _authenticationBL;


        public AuthenticationController(IAuthenticationBL authenticationBL)
        {
            _authenticationBL = authenticationBL;
        }




        // GET: Authentication
        public ActionResult LoginPage()
        {
            try
            {
                var temp = 0 / 1;
            }catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }
        public ActionResult RegisterPage()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Authenticate(CredentialModel model)
        {

            var userModel = _authenticationBL.Login(model);
            if (userModel != null)
            {
                this.Session["User"] = userModel.UserName;
                return Json(new { result = true, url = Url.Action("Index", $"{userModel.Role}") });
            }
            return Json(new { result = false, url = Url.Action("LoginPage", "Authentication") });
        }

        [HttpPost]
        public ActionResult Register()
        {
            

            return Json(new { result = true, url = "/Home/Index" /*Url.Action("Index", "Home")*/ });
        }
    }
}
