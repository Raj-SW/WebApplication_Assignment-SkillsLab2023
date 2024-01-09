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
        Task<bool> EnrolEmployeeIntoTrainingAsync(byte userId, byte trainingId, HttpFileCollectionBase FileCollection);
        #endregion

        #region Update Model
        Task<bool> ManagerApproveEnrolmentAsync(byte enrolmentId,byte userId, byte trainingId);
        Task<bool> ManagerRejectEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId, string remarks);
        void AutomaticEnrolmentProcessingForTrainingByTrainingId(byte trainingId);
        Task AutomaticEnrolmentProcessingForAllTrainingAsync();
        #endregion
    }
}
