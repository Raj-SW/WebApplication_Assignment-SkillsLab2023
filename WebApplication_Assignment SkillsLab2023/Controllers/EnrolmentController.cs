using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.CustomeServerSideValidations;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.SessionManagement;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{

    [CustomServerSideValidation]
    public class EnrolmentController : Controller
    {
        private readonly IEnrolmentBL _enrolmentBL;
        private readonly ITrainingBL _trainingBL;
        public EnrolmentController(IEnrolmentBL enrolmentBL, ITrainingBL trainingBL)
        {
            _enrolmentBL = enrolmentBL;
            _trainingBL = trainingBL;
        }
        [RoleAuthorisation("Employee")]
        [HttpPost]
        public async Task<ActionResult> EnrolEmployeeIntoTrainingAsync() 
        {
            var userId = byte.Parse(HttpContext.Request.Form["userId"]);
            var trainingId = byte.Parse(HttpContext.Request.Form["trainingId"]);
            var result = await  _enrolmentBL.EnrolEmployeeIntoTrainingAsync(userId, trainingId, Request.Files);
            if (result.isSuccess)
            {
                return Json(new { result = true, message = "Enrolment successful." });
            }
            return Json(new { result = false, message = result.GetAllResultMessageAsString() });
        }
        [RoleAuthorisation("Employee,Manager,Admin")]
        [HttpPost]
        public async Task<ActionResult> GetEnrolmentPrerequisitesByIdAsync(byte enrolmentId)
        {
            var enrolmentPrerequisites = await  _enrolmentBL.GetEnrolmentPrerequisitesOfAUserByEnrolmentIdAsync(enrolmentId);
            if (!enrolmentPrerequisites.Any())
            {
                return Json(new { result = false, message = "Internal Server Error! Could not fetch retrieved prerequisites"});
            }
            return Json(new { result = true, message = "Successfully retrieved prerequisites", EnrolmentPrerequisites = enrolmentPrerequisites });
        }
        [RoleAuthorisation("Employee")]
        [HttpPost]
        public async Task<ActionResult> isUserAlreadyRegisteredForTrainingAsync(byte trainingId, byte userId)
        {
            var result = await _enrolmentBL.isUserAlreadyRegisteredInTrainingAsync(trainingId, userId);
            if (result)
            {
                return Json(new { result = true, message = "You have Already Registered" });
            }
            return Json(new { result = false, message = "" });

        }
        [RoleAuthorisation("Manager")]
        [HttpPost]
        public async Task<ActionResult> ManagerApproveEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId)
        {
            var isSuccess = await _enrolmentBL.ManagerApproveEnrolmentAsync(enrolmentId,userId,trainingId);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Enrolment Approved" });
            }
            return Json(new { result = false, message = "There has been an error" });
        }
        [RoleAuthorisation("Manager")]
        [HttpPost]
        public async Task<ActionResult> ManagerRejectEnrolmentAsync(byte enrolmentId,string remarks,byte trainingId, byte userId)
        {
            var isSuccess = await _enrolmentBL.ManagerRejectEnrolmentAsync(enrolmentId, userId, trainingId, remarks);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Enrolment Reject successfully" });
            }
            return Json(new { result = false, message = "There has been an error " });
        }
        [HttpPost]
        public ActionResult AutomaticEnrolmentProcessingForTrainingByTrainingId(byte trainingId)
        {
            return Json(new { });
        }
        [RoleAuthorisation("Admin")]
        [HttpPost]
        public async Task<ActionResult> AutomaticEnrolmentProcessingForAllTrainingAsync()
        {
            await _enrolmentBL.AutomaticEnrolmentProcessingForAllTrainingAsync();
            return Json(new { result = true,message = "Automatic Processin for All training Triggered..." });
        }
    }
}