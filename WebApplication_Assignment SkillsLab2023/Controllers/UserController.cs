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
        private readonly IManagerActionsBL _imanageractionsbl;
        private readonly IAdminActionsBL _adminactionsbl;
        public UserController(ITrainingBL itrainingbl,IManagerActionsBL imanageractionsbl, IAdminActionsBL adminactionsbl)
        {
            _itrainingbl = itrainingbl;
            _imanageractionsbl = imanageractionsbl;
            _adminactionsbl = adminactionsbl;
        }

        public ActionResult EmployeeView()
        {
            var ListOfTrainings = _itrainingbl.GetAllTrainingModels();
            ViewBag.ListOfTrainings = ListOfTrainings;
            return View();
        }
        public ActionResult AdminView()
        {
            List<UserModel> ListOfPendingUserAccounts = _adminactionsbl.GetAllPendingUserModels();
            ViewBag.ListOfPendingUserAccounts = ListOfPendingUserAccounts;
            List<RoleModel> ListOfUserRoles = _adminactionsbl.GetAllUserRoles();
            ViewBag.ListOfUserRoles = ListOfUserRoles;
            List<ManagerDTO> ListOfManagers =_adminactionsbl.GetAllManagers();
            ViewBag.ListOfManagers = ListOfManagers;
            var ListOfDepartments = _adminactionsbl.GetAllDepartments();
            ViewBag.ListOfDepartments = ListOfDepartments;
            TrainingStatusList trainingStatusList = new TrainingStatusList();
            ViewBag.ListOFTrainingStatus=trainingStatusList.ListOfTrainingStatus;
            var ListOfPrerequisiteModel = _itrainingbl.GetAllPrerequisites();
            ViewBag.ListOfPrerequisiteModel=ListOfPrerequisiteModel;
            ViewBag.ListOfTrainingWithPrerequisites=_itrainingbl.GetAllTrainingModelsWithPrerequisites();
            //Delete Training - use of soft delete and rename all affiliated functions and alter queries
            //Update Training 
            return View();
        }
        public ActionResult ManagerView()
        {
            //TODO:
             //DATA REQUIRED; TRAINING DETAILS, TRAINING PREREQUISITES, ENROLMENT, ENROLMENT-PREREQUISITE, EMPLOYEE DETAILS
             //SEE TRAININGS
             //SEE HIS EMPLOYEES HIERARCHY
             //REVIEW HIS EMLOYEES ENROLMENT AND PERFORM ACTIONS
             //VIEW HIGH LEVEL DETAILS OF EMPLOYEE ENROLMENT
             //ON VIEW DETAILS EXPAND ENROLMENT DETAILS 
             //A WAY TO VIEW OR OPEN THE ATTACHMENTS
             //ENROL OR REJECT OR KEEP PENDING OR GIVE A FEEDBACK ON DOCUMENT ETC..
            byte ManagerId = (byte)Session["CurrentUserRoleId"];
            var listOfEmployeesEnrolment = _imanageractionsbl.GetEmployeesEnrolmentByManagerId(ManagerId);
            return View();
        }
    }
}