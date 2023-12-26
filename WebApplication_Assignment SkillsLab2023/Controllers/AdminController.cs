using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminActionsBL _adminActionsBL;
        public AdminController(IAdminActionsBL adminActionsBL)
        {
            _adminActionsBL = adminActionsBL;
        }
        [HttpPost]
        public ActionResult GetManagerByDepartment(byte DepartmentId)
        {
            var ManagersListByDepartments = _adminActionsBL.GetAllManagersByDepartmentId(DepartmentId);
            return Json(new { result= true, managers= ManagersListByDepartments });
        }
        [HttpPost]
        public ActionResult ActivatePendingAccount(ActivationDTO activationDTO) 
        {
            var isSuccess = _adminActionsBL.ActivatePendingUser(activationDTO);
            if (isSuccess)
            {
                return Json(new { result = true, message = "User Activated successfully"});
            }
            return Json(new { result = false, message = "User Activation Failed" });
        }
        [HttpPost]
        public ActionResult CreateTraining(CreateTrainingDTO createTrainingDTO) 
        {
            var isSuccess=_adminActionsBL.CreateTraining(createTrainingDTO);
            if (isSuccess)
            {
                return Json(new { result = true, message = "Training Created"});
            }
            return Json(new { result = true, message = "Training Failed to create"});
        }
    }
}