using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

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
            var userId = HttpContext.Request.Form["userId"];
            var trainingId = HttpContext.Request.Form["trainingId"];

            // Handle the files
            var files = HttpContext.Request.Files;
            foreach (string fileName in files.AllKeys)
            {
                var file = files[fileName];
                System.Diagnostics.Debug.WriteLine($"File Name: {file.FileName}");
            }
            // Your logic here...

            return Json(new { result = true });
        }
    }

}