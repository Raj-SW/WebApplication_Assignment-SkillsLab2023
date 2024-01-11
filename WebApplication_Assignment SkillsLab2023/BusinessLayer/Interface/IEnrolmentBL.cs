using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface
{
    public interface IEnrolmentBL
    {
        #region Get Model
        Task<List<GetPendingEmployeesEnrolmentOfAMangerDTO>> GetEmployeesPendingEnrolmentByManagerIdAsync(byte managerId);
        Task<List<UserPrerequisiteModel>> GetEnrolmentPrerequisitesOfAUserByEnrolmentIdAsync(byte enrolmentId);
        Task<List<UserPrerequisiteModel>> GetAllEnrolmentsManagerWiseAsync(byte ManagerId);
        Task<bool> isUserAlreadyRegisteredInTrainingAsync(byte trainingId, byte UserId);
        #endregion

        #region Insert Model
        Task<bool> EnrolEmployeeIntoTrainingAsync(byte userId, byte trainingId, HttpFileCollectionBase FileCollection);
        #endregion

        #region Update Model
        Task<bool> ManagerApproveEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId);
        Task<bool> ManagerRejectEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId, string remarks);
        Task AutomaticEnrolmentProcessingForTrainingByTrainingIdAsync(byte trainingId);
        Task AutomaticEnrolmentProcessingForAllTrainingAsync();
        Task<int> GetPrerequisiteCountOfATraining(byte trainingId);
        #endregion
    }
}

