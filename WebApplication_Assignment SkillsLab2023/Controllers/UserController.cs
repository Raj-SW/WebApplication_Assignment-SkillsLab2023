using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.CustomeServerSideValidations;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Models.Others;
using WebApplication_Assignment_SkillsLab2023.SessionManagement;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    [CustomServerSideValidation]
    public class UserController : Controller
    {
        private readonly ITrainingBL _itrainingbl;
        private readonly IUserBL _userBL;
        private readonly IDepartmentBL _departmentBL;
        private readonly IEnrolmentBL _enrolmentBL;
        public UserController(ITrainingBL itrainingbl,IUserBL userBL, IDepartmentBL departmentBL,IEnrolmentBL enrolmenBL)
        {
            _itrainingbl = itrainingbl;
            _userBL = userBL;
            _departmentBL = departmentBL;
            _enrolmentBL  = enrolmenBL;
        }

        #region View
        [RoleAuthorisation("Employee")]
        public async Task<ActionResult> EmployeeView()
        {
            var ListOfTrainings = await _itrainingbl.GetAllTrainingModelsAsync();
            ViewBag.ListOfTrainings = ListOfTrainings;
            return View();
        }
        [RoleAuthorisation("Manager")]
        public async Task<ActionResult> ManagerView()
        {
            byte ManagerId = (byte) Session["CurrentUserId"];
            var listOfEmployeesEnrolment =await _enrolmentBL.GetEmployeesPendingEnrolmentByManagerIdAsync(ManagerId);
            ViewBag.ListOfEmployeeEnrolment = listOfEmployeesEnrolment;
            return View();
        }
        [RoleAuthorisation("Admin")]
        public async Task<ActionResult> AdminView()
        {
            List<UserModel> ListOfPendingUserAccounts = await _userBL.GetAllPendingUserModelsAsync();
            ViewBag.ListOfPendingUserAccounts = ListOfPendingUserAccounts;
            List<RoleModel> ListOfUserRoles =await _userBL.GetAllUserRolesAsync();
            ViewBag.ListOfUserRoles = ListOfUserRoles;
            List<ManagerDTO> ListOfManagers =await _userBL.GetAllManagersAsync();
            ViewBag.ListOfManagers = ListOfManagers;
            var ListOfDepartments =await _departmentBL.GetAllDepartmentsAsync();
            ViewBag.ListOfDepartments = ListOfDepartments;
            TrainingStatusList trainingStatusList = new TrainingStatusList();
            ViewBag.ListOFTrainingStatus=trainingStatusList.ListOfTrainingStatus;
            var ListOfPrerequisiteModel = await _itrainingbl.GetAllPrerequisitesAsync();
            ViewBag.ListOfPrerequisiteModel=ListOfPrerequisiteModel;
            ViewBag.ListOfTrainingWithPrerequisites= await _itrainingbl.GetAllTrainingModelsWithPrerequisitesAsync();
            ViewBag.ListOfUserModelsAndTheirRoles=await _userBL.GetAllUsersAndTheirRolesAsync();
            return View();
        }

        #endregion

        #region Get Model
        [RoleAuthorisation("Admin")]
        public async Task<ActionResult> GetAllManagersAsync()
        {
            var ListOfManagers=await _userBL.GetAllManagersAsync();
            return Json(new {result = true, ListOfManagers=ListOfManagers});
        }
        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> GetAllManagersByDepartmentIdAsync(byte departmentId) {
            var ManagersListByDepartments =await _userBL.GetAllManagersByDepartmentIdAsync(departmentId);
            return Json(new { result = true, managers = ManagersListByDepartments });
        }
        [RoleAuthorisation("Manager")]
        public async Task<ActionResult> GetAllPendingUserModelsAsync()
        {
            var ListOfPendingUserModels =await _userBL.GetAllPendingUserModelsAsync();
            return Json(new { result = true, ListOfPendingUserModels = ListOfPendingUserModels });
        }
        public async Task<ActionResult> GetAllUserRolesAsync()
        {
            var ListOfAllUserRoles =await _userBL.GetAllUserRolesAsync();
            return Json(new { result = true, ListOfAllUserRoles = ListOfAllUserRoles });
        }
        #endregion

        #region Insert 

        #endregion

        #region Update
        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> ActivatePendingAccountAsync(ActivationDTO activationDTO)
        {
            var isSuccess = await _userBL.ActivatePendingUserAsync(activationDTO);
            if (isSuccess)
            {
                return Json(new { result = true, message = "User Activated successfully" });
            }
            return Json(new { result = false, message = "User Activation Failed" });
        }
        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> UpdateUserAndRolesAsync(UserAndRolesDTO dto) 
        {
            var result = await _userBL.UpdateUserAndRolesAsync(dto);
            if (result)
            {
                return Json(new { result = true, message = "User Updated Successfully" });
            }
            return Json(new { result = false, message = "User Failed to Update" });
        }
        #endregion

        #region Delete

        #endregion
    }
}