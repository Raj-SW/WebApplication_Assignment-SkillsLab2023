using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.Models;


namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ITrainingBL _itrainingbl;
 
        public EmployeeController(ITrainingBL itrainingbl)
        {
            _itrainingbl = itrainingbl;
        }

        public ActionResult Index()
        {
            List<TrainingModel> ListOfTrainings = _itrainingbl.GetAllTraining();
            ViewBag.ListOfTrainings = ListOfTrainings;
            return View();
        }
    }
}