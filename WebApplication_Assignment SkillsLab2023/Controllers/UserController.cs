using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Models.Others;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
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
        public ActionResult EmployeeView()
        {
            var ListOfTrainings = _itrainingbl.GetAllTrainingModels();
            ViewBag.ListOfTrainings = ListOfTrainings;
            return View();
        }
        public ActionResult AdminView()
        {
            List<UserModel> ListOfPendingUserAccounts = _userBL.GetAllPendingUserModels();
            ViewBag.ListOfPendingUserAccounts = ListOfPendingUserAccounts;
            List<RoleModel> ListOfUserRoles = _userBL.GetAllUserRoles();
            ViewBag.ListOfUserRoles = ListOfUserRoles;
            List<ManagerDTO> ListOfManagers = _userBL.GetAllManagers();
            ViewBag.ListOfManagers = ListOfManagers;
            var ListOfDepartments = _departmentBL.GetAllDepartments();
            ViewBag.ListOfDepartments = ListOfDepartments;
            TrainingStatusList trainingStatusList = new TrainingStatusList();
            ViewBag.ListOFTrainingStatus=trainingStatusList.ListOfTrainingStatus;
            var ListOfPrerequisiteModel = _itrainingbl.GetAllPrerequisites();
            ViewBag.ListOfPrerequisiteModel=ListOfPrerequisiteModel;
            ViewBag.ListOfTrainingWithPrerequisites=_itrainingbl.GetAllTrainingModelsWithPrerequisites();
            ViewBag.ListOfUserModelsAndTheirRoles=_userBL.GetAllUsersAndTheirRoles();
            return View();
        }
        public ActionResult ManagerView()
        {
            //TODO:
            //DATA REQUIRED; TRAINING DETAILS, TRAINING PREREQUISITES, ENROLMENT, ENROLMENT-PREREQUISITE, EMPLOYEE DETAILS
            //SEE TRAININGS
            //SEE HIS EMPLOYEES HIERARCHY

            //REVIEW HIS EMLOYEES ENROLMENT AND PERFORM ACTIONS
            //ON VIEW DETAILS EXPAND ENROLMENT DETAILS 
            //A WAY TO VIEW OR OPEN THE ATTACHMENTS
            //ENROL OR REJECT OR KEEP PENDING OR GIVE A FEEDBACK ON DOCUMENT ETC..
            byte ManagerId = (byte) Session["CurrentUserId"];
            var listOfEmployeesEnrolment = _enrolmentBL.GetEmployeesPendingEnrolmentByManagerId(ManagerId);
            ViewBag.ListOfEmployeeEnrolment = listOfEmployeesEnrolment;
            return View();
        }

        #endregion

        #region Get Model
        public ActionResult GetAllManagers()
        {
            var ListOfManagers= _userBL.GetAllManagers();
            return Json(new {result = true, ListOfManagers=ListOfManagers});
        }
        [HttpPost]
        public ActionResult GetAllManagersByDepartmentId(byte departmentId) {
            var ManagersListByDepartments = _userBL.GetAllManagersByDepartmentId(departmentId);
            return Json(new { result = true, managers = ManagersListByDepartments });
        }
        public ActionResult GetAllPendingUserModels()
        {
            var ListOfPendingUserModels = _userBL.GetAllPendingUserModels();
            return Json(new { result = true, ListOfPendingUserModels = ListOfPendingUserModels });
        }
        public ActionResult GetAllUserRoles()
        {
            var ListOfAllUserRoles = _userBL.GetAllUserRoles();
            return Json(new { result = true, ListOfAllUserRoles = ListOfAllUserRoles });
        }
        #endregion

        #region Insert 

        #endregion

        #region Update
        [HttpPost]
        public ActionResult ActivatePendingAccount(ActivationDTO activationDTO)
        {
            var isSuccess = _userBL.ActivatePendingUser(activationDTO);
            if (isSuccess)
            {
                return Json(new { result = true, message = "User Activated successfully" });
            }
            return Json(new { result = false, message = "User Activation Failed" });
        }
        [HttpPost]
        public ActionResult UpdateUserAndRoles(UserAndRolesDTO dto) 
        {
            var result = _userBL.UpdateUserAndRoles(dto);
            if (result)
            {
                return Json(new { result = true, message = "User Updated Successfully" }); ;
            }
            return Json(new { result = false, message = "User Failed to Update" }); ;
        }
        #endregion

        #region Delete

        #endregion
    }
}