using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface IManagerActionsDAL
    {
        List<UserModel> GetEmployeesPendingEnrolmentByManagerId(int managerId);
        void RejectEmployeeEnrolment();
        void ApproveEmployeeEnrolment();
    }
}
