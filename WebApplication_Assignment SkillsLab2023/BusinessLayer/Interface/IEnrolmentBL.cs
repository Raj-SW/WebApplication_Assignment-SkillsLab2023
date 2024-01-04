using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface
{
    public interface IEnrolmentBL
    {
        #region Get Model
        List<GetPendingEmployeesEnrolmentOfAMangerDTO> GetEmployeesPendingEnrolmentByManagerId(byte managerId);
        List<UserPrerequisiteModel> GetEnrolmentPrerequisitesOfAUserByEnrolmentId(byte enrolmentId);
        List<UserPrerequisiteModel> GetAllEnrolmentsManagerWise(byte ManagerId);
        bool isUserAlreadyRegisteredInTraining(byte trainingId, byte UserId);
        #endregion

        #region Insert Model
        bool EnrolEmployeeIntoTraining(int userId, int trainingId, HttpFileCollectionBase FileCollection);
        #endregion

        #region Update Model
        bool ApproveEnrolment(byte enrolmentId);
        bool RejectEnrolment(byte enrolmentId, string remarks);
        #endregion
    }
}
