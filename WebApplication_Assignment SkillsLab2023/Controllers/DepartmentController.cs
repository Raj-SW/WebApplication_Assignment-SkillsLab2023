using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;

namespace WebApplication_Assignment_SkillsLab2023.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentBL _departmentBL;
        public DepartmentController(IDepartmentBL departmentBL) 
        {
        _departmentBL=departmentBL;
        }

        public async Task<ActionResult> GetAllDepartmentsAsync()
        {
            var ListOfDepartment = await _departmentBL.GetAllDepartmentsAsync();
            return View();
        }
    }
}