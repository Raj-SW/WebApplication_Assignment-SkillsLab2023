using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
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
        public ActionResult Index()
        {
            return View();
        }
        public  ActionResult GetTraining() 
        {
            return null;
        }
        public ActionResult GetAllTraining()
        {
            var ListOftraining = _trainingBl.GetAllTrainingModels();
            ViewBag.ListOfTraining=ListOftraining;
            return Json(new { result = true, listOfTraining = ListOftraining}) ;
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
        [HttpPost]
        public ActionResult CreateTraining(CreateTrainingDTO createTrainingDTO)
        {
            var isSuccess = _trainingBl.CreateTraining(createTrainingDTO);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training Created" });
            }
            return Json(new { result = true, message = "Training Failed to create" });
        }
        [HttpPost]
        public ActionResult UpdateTraining(TrainingModel trainingModel)
        {
            var isSuccess= _trainingBl.UpdateTraining(trainingModel);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training Updated Successfully" });
            }
                return Json(new { result = false, message = "Training Update Unsuccessfully" });
        }
        [HttpPost]
        public ActionResult AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            var isSucces = _trainingBl.AddPrerequisiteToTraining(trainingPrerequisiteModel);
            if (isSucces)
            {
                return Json(new { result = true, message = "Training Prerequisite Added to training successfully" });
            }
            return Json(new { result = false, message = "Training Prerequisite not being added" });
        }
        [HttpPost]
        public ActionResult UpdatePrerequisiteInTraining(byte TrainingId,List<byte> Prerequisites) 
        {
            var isSuccess = _trainingBl.UpdateTrainingPrerequisite(TrainingId,Prerequisites);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training Prerequisite successfully updated" }); ;
            }
            return Json(new { result = false, message = "Training Prerequisite update unsuccessful" });
        }
        [HttpPost]
        public ActionResult DeleteTraining(byte id) 
        {
            var isSucces = _trainingBl.DeleteTraining(id);
            if (isSucces)
            {
                return Json(new { result = true, message="Training deleted successfully" }); ;
            }
            return Json(new { result = false, message = "Training deletion unsuccessful" }); ;
        }
    }
}