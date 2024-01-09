using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL.Interface;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class DepartmentBL :IDepartmentBL
    {
        private readonly IDepartmentDAL _departmentDAL;
        public DepartmentBL(IDepartmentDAL departmentDAL)
        {
            _departmentDAL = departmentDAL;
        }
        #region Get Models
        public async Task<List<DepartmentModel>> GetAllDepartmentsAsync()
        {
            return await _departmentDAL.GetAllDepartmentsAsync();
        }
        #endregion

    }
}