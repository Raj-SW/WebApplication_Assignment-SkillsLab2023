using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IEnrolmentBL _enrolmentBL;
        public EnrolmentController(IEnrolmentBL enrolmentBL)
        {
            _enrolmentBL = enrolmentBL;
        }

        [HttpPost]
        public async Task<ActionResult> EnrolEmployeeIntoTraining() 
        {
            var userId = byte.Parse(HttpContext.Request.Form["userId"]);
            var trainingId = byte.Parse(HttpContext.Request.Form["trainingId"]);
            var result = await  _enrolmentBL.EnrolEmployeeIntoTrainingAsync(userId, trainingId, Request.Files);
            if (result)
            {
                return Json(new { result = true, message = "Enrolment successful." });
            }
            return Json(new { result = false, message = "Enrolment failed. Make sure you have submitted all files required" });
        }
        [HttpPost]
        public ActionResult GetEnrolmentPrerequisitesById(byte enrolmentId)
        {
            var enrolmentPrerequisites = _enrolmentBL.GetEnrolmentPrerequisitesOfAUserByEnrolmentId(enrolmentId);
            return Json(new { result = true, message = "Successfully retrieved prerequisites", EnrolmentPrerequisites = enrolmentPrerequisites });
        }
        [HttpPost]
        public ActionResult isUserAlreadyRegisteredForTraining(byte trainingId, byte userId) {
            var result = _enrolmentBL.isUserAlreadyRegisteredInTraining(trainingId, userId);
            if (result)
            {
                return Json(new { result = true, message = "You have Already Registered" });
            }
            return Json(new { result = false, message = "" });

        }
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
    }
}