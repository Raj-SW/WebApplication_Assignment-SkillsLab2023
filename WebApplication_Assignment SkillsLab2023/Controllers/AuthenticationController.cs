using System.Web.Mvc;
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
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult RegistrationPage()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LoginUser(CredentialModel model)
        {
            UserModel userModel = _authenticationBL.LoginUser(model);
            if (userModel != null)
            {
                Session["User"] = userModel.UserName;
                return Json(new { result = true, url = Url.Action("Index", $"{userModel.Role}"),user=userModel,message="Successful login" });
            }
            return Json(new { result = false, url = Url.Action("LoginPage", "Authentication"),user= userModel,message = "User Id or Password wrong" });
        }
        [HttpPost]
        public ActionResult RegisterUser()
        {
            return Json(new { result = true, url = "/Home/Index"});
        }
    }
}
