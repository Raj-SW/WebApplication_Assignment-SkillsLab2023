using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult GetAllDepartments()
        {
            var ListOfDepartment=_departmentBL.GetAllDepartments();
            return View();
        }
    }
}