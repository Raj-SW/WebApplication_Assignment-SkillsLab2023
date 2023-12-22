using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationBL _authenticationBL;
        private readonly IUserBL _userBL;
        public AuthenticationController(IAuthenticationBL authenticationBL,IUserBL userBL)
        {
            _authenticationBL = authenticationBL;
            _userBL = userBL;

        }
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult RegistrationPage()
        {
            return View();
        }
        public ActionResult RoleSelectionPage()
        {
            var listOfRoles = Session["UserRoles"] as List<UserRolesModel>;
            return View(listOfRoles);
        }
        [HttpPost]
        public ActionResult RedirectToUserRole(int roleId)
        {
            List<RoleModel> listOfRoles= _authenticationBL.GetAllRoles();
            foreach (RoleModel role in listOfRoles)
            {
                if (role.RoleId == roleId)
                {
                    Session["CurrentUserRoleId"] = role.RoleId;
                    Session["CurrentUserRoleName"] = role.RoleName;
                    return Json(new { result = true, url = $"/User/{role.RoleName}View", message = "Redirection Successful" });
                }
            }
            return Json(new { success = false, message = "Internal Server Error. Your selectde Role was not found" });
        }
        [HttpPost]
        public ActionResult LoginUser(CredentialModel model)
        {
            DataModelResult<UserModel> UserDataModelResult= _authenticationBL.LoginUser(model);
            if (UserDataModelResult.ResultTask.isSuccess)
            {
                Session["CurrentUserName"] = UserDataModelResult.ResultObject.UserFirstName;
                Session["CurrentUserId"] = UserDataModelResult.ResultObject.UserId;
                Session["UserRoles"] = _userBL.GetAllUserRolesModelByUserId(UserDataModelResult.ResultObject.UserId);
                return Json(new { result = true, url = Url.Action("RoleSelectionPage", "Authentication"), user = UserDataModelResult.ResultObject, message = UserDataModelResult.ResultTask.GetAllResultMessageAsString() });
            }
            return Json(new { result = true, url = Url.Action("LoginPage", "Authentication"), message = UserDataModelResult.ResultTask.GetAllResultMessageAsString() });
        }
        [HttpPost]                      
        public ActionResult RegisterUser(UserAndCredentialDTO model)
        {
            TaskResult taskresult = _authenticationBL.RegisterUser(model);
            if (taskresult.isSuccess)
            {
                return Json(new { result = taskresult.isSuccess, url = "/Authentication/LoginPage", message = taskresult.GetAllResultMessageAsString() });
            }
            return Json(new { result = taskresult.isSuccess, url = "/Authentication/RegistrationPage", message =taskresult.GetAllResultMessageAsString() });
        }
    }
}
