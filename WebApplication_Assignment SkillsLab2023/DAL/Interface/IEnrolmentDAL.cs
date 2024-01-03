using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL.Interface
{
    public interface IEnrolmentDAL
    {
        #region Get Model
        List<GetPendingEmployeesEnrolmentOfAMangerDTO> GetEmployeesPendingEnrolmentByManagerId(byte managerId);
        List<UserPrerequisiteModel> GetEnrolmentPrerequisitesOfAUserByEnrolmentId(byte enrolmentId);
        List<UserPrerequisiteModel> GetAllEnrolmentsManagerWise(byte ManagerId);
        #endregion

        #region Insert Models
        bool EnrolEmployeeIntoTraining(int userId, int trainingId, List<string> filepath);
        #endregion

        #region Update Models
        bool ApproveEnrolment(byte enrolmentId);
        bool RejectEnrolment(byte enrolmentId, string remarks);
        #endregion
    }
}
