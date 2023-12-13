using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingBL _trainingBl;
         public TrainingController(ITrainingBL trainingBl)
        {
            _trainingBl = trainingBl;

        }
        // GET: Training
        public ActionResult Index()
        {
            return View();
        }
        public  ActionResult GetTraining() 
        {
            return null;

        }
        public ActionResult GetTrainingPrerequisite()
        {
            return null;
        }

        [HttpPost]
        public JsonResult GetTrainingPrerequisitebyID(int trainingId)
        {
            List<TrainingPrerequisiteModel> trainingPrerequisiteModelListById = _trainingBl.GetTrainingPrerequisitesById(trainingId);
            return Json(new { result = true, preReqList=trainingPrerequisiteModelListById});
        }
        [HttpPost]
        public JsonResult EnrolEmployeeIntoTraining()
        {

            return Json(new { result = true });
        }
    }

}