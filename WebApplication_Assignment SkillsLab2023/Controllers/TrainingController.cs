using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

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
            var userId = int.Parse(HttpContext.Request.Form["userId"]);
            var trainingId = int.Parse(HttpContext.Request.Form["trainingId"]);
            var result = _trainingBl.EnrolEmployeeIntoTraining(userId,trainingId,Request.Files);
            if (result)
            {
                return Json(new {result=true, message="Enrolment successful." });
            }
            return Json(new { result = false,message="Enrolment failed" });
        }
    }

}