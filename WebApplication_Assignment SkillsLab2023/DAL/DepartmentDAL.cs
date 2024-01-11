using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
        public async Task<List<DepartmentModel>> GetAllDepartmentsAsync()
        {
            const string GET_ALL_DEPARTMENTS_QUERY = @"SELECT * FROM [Department]";
            var result = await _command.GetDataAsync<DepartmentModel>(GET_ALL_DEPARTMENTS_QUERY);
            return result;
        }
    }
}