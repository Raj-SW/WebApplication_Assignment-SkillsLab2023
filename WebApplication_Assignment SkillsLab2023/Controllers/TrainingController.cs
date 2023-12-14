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

            //this also works
            // Handle the files
            // var files = HttpContext.Request.Files;

            //foreach (string fileName in files.AllKeys)
            //{
            //    var file = files[fileName];
            //    System.Diagnostics.Debug.WriteLine($"File Name from HttpContext: {file.FileName}");
            //}

            //////////////////////////////////
            // Your logic here..

            //foreach (string fileName in Request.Files)
            //{
            //    HttpPostedFileBase file = Request.Files[fileName];

            //    if (file != null && file.ContentLength > 0)
            //    {
            //        // Handle each file as needed
            //        var uploadedFileName = Path.GetFileName(file.FileName);
            //        System.Diagnostics.Debug.WriteLine($"File Name: {uploadedFileName}");
            //    }
            //}

            var result = _trainingBl.EnrolEmployeeIntoTraining(userId,trainingId,Request.Files);


            return Json(new { result = true });
        }
    }

}