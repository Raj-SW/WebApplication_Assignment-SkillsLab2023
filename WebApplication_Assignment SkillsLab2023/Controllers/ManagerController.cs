using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.Custom;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class ManagerController : Controller
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
        public ActionResult ManagerView()
        {
            return View();
        }
    }
}