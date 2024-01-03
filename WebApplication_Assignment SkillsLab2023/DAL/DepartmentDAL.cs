using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.DAL.Interface;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class DepartmentDAL : IDepartmentDAL
    {
        private readonly IDBCommand _command;
        public DepartmentDAL(IDBCommand command) { _command = command; }
        public List<DepartmentModel> GetAllDepartments()
        {
            const string GET_ALL_DEPARTMENTS_QUERY = @"SELECT * FROM [Department]";
            List<DepartmentModel> listOfDepartments = new List<DepartmentModel>();
            DepartmentModel departmentModel;
            var dt = _command.GetData(GET_ALL_DEPARTMENTS_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                departmentModel = new DepartmentModel();
                departmentModel.DepartmentId = (byte)row["DepartmentId"];
                departmentModel.DepartmentName = (string)row["DepartmentName"];
                departmentModel.NoOfEmployees = (byte)row["NoOfEmployees"];
                listOfDepartments.Add(departmentModel);
            }
            return listOfDepartments;
        }
    }
}