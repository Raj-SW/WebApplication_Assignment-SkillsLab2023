using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class ManagerActionsBL : IManagerActionsBL
    {
        private readonly IManagerActionsDAL _managerActionsDAL;
        public ManagerActionsBL(IManagerActionsDAL managerActionsDAL) 
        {
            _managerActionsDAL = managerActionsDAL;
        }
        public void ApproveEmployeeEnrolment()
        {
            throw new NotImplementedException();
        }
        public List<UserModel> GetEmployeesEnrolmentByManagerId(int managerId)
        {

            return _managerActionsDAL.GetEmployeesPendingEnrolmentByManagerId(managerId);
        }
        public void RejectEmployeeEnrolment()
        {
            throw new NotImplementedException();
        }
    }
}