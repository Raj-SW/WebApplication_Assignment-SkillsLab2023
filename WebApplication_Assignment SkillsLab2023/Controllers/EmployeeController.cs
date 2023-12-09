using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023;
using WebApplication_Assignment_SkillsLab2023.Custom;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ITrainingBL _itrainingbl;
 
        public EmployeeController(ITrainingBL itrainingbl)
        {
            _itrainingbl = itrainingbl;
        }

        // GET: Employee
        //[CustomAuthorizationAttribute("Employee")]
        public ActionResult Index()
        {
            List<TrainingModel> ListOfTrainings = _itrainingbl.GetAllTraining();
            ViewBag.ListOfTrainings = ListOfTrainings;
            return View();
        }
    }
}