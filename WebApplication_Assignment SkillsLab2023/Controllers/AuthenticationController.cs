using System.Collections.Generic;
using System.Web.Http.Results;
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
        public ActionResult LoginUser(CredentialModel model)
        {
            DataModelResult<UserModel> UserDataModelResult= _authenticationBL.LoginUser(model);
            if (UserDataModelResult.ResultTask.isSuccess)
            {
                Session["CurrentUser"] = UserDataModelResult.ResultObject.UserFirstName;
                //Session["CurrentRole"] = UserDataModelResult.ResultObject.Role;
                Session["CurrentUserId"] = UserDataModelResult.ResultObject.UserId;
                //TODO:
                //GET ROLES
                //REDIRECT TO ACCORDING VIEW
                UserRolesModel roles = new UserRolesModel();
                roles.Roles= _authenticationBL.GetUserRolesByUserId(UserDataModelResult.ResultObject.UserId);
                //return Json(new { result = true, url = Url.Action("Index", $"{UserDataModelResult.ResultObject.Role}"), user = UserDataModelResult.ResultObject, message = UserDataModelResult.ResultTask.GetAllResultMessageAsString() });
                TempData["UserRoles"] = roles;
                return RedirectToAction("RoleSelection");
            }
            return Json(new { result = true, url = Url.Action("LoginPage", "Authentication"), message = UserDataModelResult.ResultTask.GetAllResultMessageAsString() });
        }
        [HttpPost]                      
        public ActionResult RegisterUser(RegistrationDTO model)
        {
            TaskResult taskresult = _authenticationBL.RegisterUser(model);
            if (taskresult.isSuccess)
            {
                return Json(new { result = taskresult.isSuccess, url = "/Authentication/LoginPage", message = taskresult.GetAllResultMessageAsString() });
            }
            return Json(new { result = taskresult.isSuccess, url = "/Authentication/RegistrationPage", message =taskresult.GetAllResultMessageAsString() });
        }
        [HttpPost]
        public ActionResult RoleSelection()
        {
            UserRolesModel Roles = TempData["UserRoles"] as UserRolesModel;
            return View(Roles);
        }
    }
}
