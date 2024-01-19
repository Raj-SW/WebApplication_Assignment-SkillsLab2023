using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.CustomeServerSideValidations;
using WebApplication_Assignment_SkillsLab2023.SessionManagement;
using System;
using System.Linq;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    [UserSession]
    [CustomServerSideValidation]
    public class TrainingController : Controller
    {
        private readonly ITrainingBL _trainingBl;
        public TrainingController(ITrainingBL trainingBl)
        {
            _trainingBl = trainingBl;
        }

        #region Get Model

        [RoleAuthorisation("Admin")]
        public async Task<ActionResult> GetAllTrainingAsync()
        {
            var listOfTraining = await _trainingBl.GetAllTrainingModelsAsync();
            ViewBag.ListOfTraining = listOfTraining;
            return Json(new { result = true, listOfTraining });
        }
        [RoleAuthorisation("Employee,Manager,Admin")]
        [HttpPost]
        public async Task<JsonResult> GetTrainingPrerequisitebyTrainingIdAsync(int trainingId)
        {
            List<PrerequisitesModel> trainingPrerequisiteModelListById = await _trainingBl.GetTrainingPrerequisitesByIdAsync(trainingId);
            return Json(new { result = true, preReqList = trainingPrerequisiteModelListById });
        }
        [RoleAuthorisation("Manager")]
        public ActionResult GetFile(string filePath)
        {
            var fullPath = Path.Combine(Server.MapPath("~/"), filePath);

            if (System.IO.File.Exists(fullPath))
            {
                var fileContents = System.IO.File.ReadAllBytes(fullPath);
                var mimeType = MimeMapping.GetMimeMapping(filePath);
                Response.AppendHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(filePath));
                return File(fileContents, mimeType);
            }
            else
            {
                return HttpNotFound();
            }
        }
        #endregion

        #region Insert

        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateTrainingAsync(CreateTrainingDTO createTrainingDTO)
        {
            var isTrainingExistedAlready = await _trainingBl.IsTrainingUnique(createTrainingDTO.TrainingName);
            if (isTrainingExistedAlready)
            {
                return Json(new { result = false, message = "There is already a training of that Name created" });
            }
            var repeatedElements = createTrainingDTO.Prerequisites.GroupBy(x => x)
                                            .Where(group => group.Count() > 1)
                                            .Select(group => group.Key)
                                            .ToList();
            if (repeatedElements.Any())
            {
                return Json(new { result = false, message = "The same prerequisites are added twice" });
            }
            var isSuccess = await _trainingBl.CreateTrainingAsync(createTrainingDTO);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training Created" });
            }
            return Json(new { result = false, message = "Training Failed to create" });
        }
        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> AddPrerequisiteToTrainingAsync(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            var isSuccess = await _trainingBl.AddPrerequisiteToTrainingAsync(trainingPrerequisiteModel);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training Prerequisite Added to training successfully" });
            }
            return Json(new { result = false, message = "Training Prerequisite not being added" });
        }
        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> CreatePrerequisitesAsync(string prerequisiteDescription)
        {
            var isSuccess = await _trainingBl.CreatePrerequisiteAsync(prerequisiteDescription);
            return Json(new { result = true, message = "Prerequisite Added Successfully" });
        }

        #endregion

        #region Update

        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> UpdateTrainingAsync(UpdateTrainingDTO updateTrainingDTO)
        {
            var isSuccess = await _trainingBl.UpdateTrainingAsync(updateTrainingDTO);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training Updated Successfully" });
            }
            return Json(new { result = false, message = "Training Update Unsuccessful" });
        }

        #endregion

        #region Delete

        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteTrainingAsync(byte trainingId)
        {
            var isSuccess = await _trainingBl.DeleteTrainingAsync(trainingId);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training deleted successfully" });
            }
            return Json(new { result = false, message = "Training deletion unsuccessful. There might be enrolments" });
        }

        #endregion
    }
}
