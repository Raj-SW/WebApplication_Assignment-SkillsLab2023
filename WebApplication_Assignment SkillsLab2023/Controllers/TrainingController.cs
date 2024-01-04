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
        #region Get Model
        public ActionResult GetAllTraining()
        {
            var ListOftraining = _trainingBl.GetAllTrainingModels();
            ViewBag.ListOfTraining=ListOftraining;
            return Json(new { result = true, listOfTraining = ListOftraining}) ;
        }
        [HttpPost]
        public JsonResult GetTrainingPrerequisitebyID(int trainingId)
        {
            List<PrerequisitesModel> trainingPrerequisiteModelListById = _trainingBl.GetTrainingPrerequisitesById(trainingId);
            return Json(new { result = true, preReqList=trainingPrerequisiteModelListById});
        }
        #endregion

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
        public ActionResult CreatePrerequisites(string prerequisiteDescription) 
        {
            var isSuccess = _trainingBl.CreatePrerequisite(prerequisiteDescription);
            return Json(new { result = true, message="Prerequisite Added Successfully" });
        }
    }
}