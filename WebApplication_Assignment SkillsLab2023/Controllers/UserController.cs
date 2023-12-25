using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

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

            List<TrainingModel> ListOfTrainings = _itrainingbl.GetAllTraining();
            ViewBag.ListOfTrainings = ListOfTrainings;
            return View();
        }
        public ActionResult AdminView()
        {
            //TODO
            //Activate Pending User Account
                //Assign the User to a Department and a Manager
                    //Get All Pending User - Done
            List<UserModel> ListOfPendingUserAccounts = _adminactionsbl.GetAllPendingUserModels();
            ViewBag.ListOfPendingUserAccounts = ListOfPendingUserAccounts;
                    //Get All Roles - done
            List<RoleModel> ListOfUserRoles = _adminactionsbl.GetAllUserRoles();
            ViewBag.ListOfUserRoles = ListOfUserRoles;
            //Get All Managers - done
            List<ManagerDTO> ListOfManagers =_adminactionsbl.GetAllManagers();
            ViewBag.ListOfManagers = ListOfManagers;
            //Get All Department - done
            var ListOfDepartments = _adminactionsbl.GetAllDepartments();
            ViewBag.ListOfDepartments = ListOfDepartments;
            //Create Training
            //Delete Training
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