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
        public ActionResult DeleteTraining(byte trainingId) 
        {
            var isSucces = _trainingBl.DeleteTraining(trainingId);
            if (isSucces)
            {
                return Json(new { result = true, message="Training deleted successfully" }); ;
            }
            return Json(new { result = false, message = "Training deletion unsuccessful.There might be enrolments" }); ;
        }
        [HttpPost]
        public ActionResult GetEnrolmentPrerequisitesById(byte enrolmentId)
        {
            var enrolmentPrerequisites = _trainingBl.GetEnrolmentPrerequisitesOfAUserByEnrolmentId(enrolmentId);
            return Json(new { result = true, message = "Successfully retrieved prerequisites", EnrolmentPrerequisites= enrolmentPrerequisites });
        }
        public ActionResult GetFile(string filePath)
        {
            var fullPath = Path.Combine(Server.MapPath("~/"), filePath);

            if (System.IO.File.Exists(fullPath))
            {
                var fileContents = System.IO.File.ReadAllBytes(fullPath);
                var mimeType = MimeMapping.GetMimeMapping(filePath);

                // Set the Content-Disposition header to "inline" for the file to be displayed in the browser
                Response.AppendHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(filePath));

                return File(fileContents, mimeType);
            }
            else
            {
                // Handle file not found appropriately
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ApproveEnrolment(byte enrolmentId)
        {
            var isSuccess = _trainingBl.ApproveEnrolment(enrolmentId);
            if (isSuccess)
            {
                return Json(new { result = true, message="Enrolment Approved"});
            }
            return Json(new { result = false, message = "There has been an error" });

        }
        [HttpPost]
        public ActionResult RejectEnrolment(byte enrolmentId,string remarks)
        {
            var isSuccess =_trainingBl.RejectEnrolment(enrolmentId, remarks);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Enrolment Reject successfully" });

            }
            return Json(new { result = false, message = "There has been an error " });

        }
        [HttpPost]
        public ActionResult CreatePrerequisites(string prerequisiteDescription) 
        {
            var isSuccess = _trainingBl.CreatePrerequisite(prerequisiteDescription);
            return Json(new { result = true, message="Prerequisite Added Successfully" });
        }
    }
}