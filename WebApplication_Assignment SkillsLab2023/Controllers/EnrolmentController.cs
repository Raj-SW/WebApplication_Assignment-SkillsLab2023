using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult ApproveEnrolment(byte enrolmentId)
        {
            var isSuccess = _enrolmentBL.ApproveEnrolment(enrolmentId);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Enrolment Approved" });
            }
            return Json(new { result = false, message = "There has been an error" });
        }
        [HttpPost]
        public ActionResult RejectEnrolment(byte enrolmentId, string remarks)
        {
            var isSuccess = _enrolmentBL.RejectEnrolment(enrolmentId, remarks);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Enrolment Reject successfully" });

            }
            return Json(new { result = false, message = "There has been an error " });

        }
    }
}