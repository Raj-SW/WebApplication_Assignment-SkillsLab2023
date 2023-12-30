using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class ManagerActionsDAL : IManagerActionsDAL
    {
        public void ApproveEmployeeEnrolment()
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetEmployeesPendingEnrolmentByManagerId(int managerId)
        {
            const string GET_EMPLOYEES_PENDING_ENROLMENT_BY_MANAGER_ID_QUERY = @"SELECT *";

            List<UserModel> employeesEnrolment =new List<UserModel>();
            UserModel employee;
            throw new NotImplementedException();
        }

        public void RejectEmployeeEnrolment()
        {
            throw new NotImplementedException();
        }
    }
}