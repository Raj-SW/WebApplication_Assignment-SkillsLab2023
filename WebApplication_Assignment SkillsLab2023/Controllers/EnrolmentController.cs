﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult EnrolEmployeeIntoTraining() 
        {
            var userId = int.Parse(HttpContext.Request.Form["userId"]);
            var trainingId = int.Parse(HttpContext.Request.Form["trainingId"]);
            var result = _enrolmentBL.EnrolEmployeeIntoTraining(userId, trainingId, Request.Files);
            if (result)
            {
                return Json(new { result = true, message = "Enrolment successful." });
            }
            return Json(new { result = false, message = "Enrolment failed" });
        }
        [HttpPost]
        public ActionResult GetEnrolmentPrerequisitesById(byte enrolmentId)
        {
            var enrolmentPrerequisites = _enrolmentBL.GetEnrolmentPrerequisitesOfAUserByEnrolmentId(enrolmentId);
            return Json(new { result = true, message = "Successfully retrieved prerequisites", EnrolmentPrerequisites = enrolmentPrerequisites });
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