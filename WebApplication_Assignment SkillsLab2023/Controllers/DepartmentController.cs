using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.CustomeServerSideValidations;
using WebApplication_Assignment_SkillsLab2023.SessionManagement;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    [CustomServerSideValidation]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentBL _departmentBL;
        public DepartmentController(IDepartmentBL departmentBL) 
        {
         _departmentBL=departmentBL;
        }
        [RoleAuthorisation("Admin")]
        public async Task<ActionResult> GetAllDepartmentsAsync()
        {
            var ListOfDepartment = await _departmentBL.GetAllDepartmentsAsync();
            return View();
        }
    }
}