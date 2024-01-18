using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL.Interface
{
    public interface IEnrolmentDAL
    {
        #region Get Model
        Task<List<GetPendingEmployeesEnrolmentOfAMangerDTO>> GetEmployeesPendingEnrolmentByManagerIdAsync(byte managerId);
        Task<List<UserPrerequisiteModel>> GetEnrolmentPrerequisitesOfAUserByEnrolmentIdAsync(byte enrolmentId);
        Task<List<UserPrerequisiteModel>> GetAllEnrolmentsManagerWiseAsync(byte ManagerId);
        Task<bool> isUserAlreadyRegisteredInTrainingAsync(byte trainingId, byte UserId);
        #endregion

        #region Insert Models
        Task<bool> EnrolEmployeeIntoTrainingAsync(byte userId, byte trainingId, List<string> filepath);
        #endregion

        #region Update Models
        Task<bool> ManagerApproveEnrolmentAsync(byte enrolmentId);
        Task<bool> ManagerRejectEnrolmentAsync(byte enrolmentId, string remarks);
        Task AutomaticEnrolmentProcessingForTrainingByTrainingIdAsync(byte trainingId);
        Task<List<AutomaticProcessingDTO>> AutomaticEnrolmentProcessingForAllTrainingAsync();
        Task<List<EmployeeEnrolmentOverviewDTO>> GetAllEmployeesEnrolmentHistoryOfAManagerByIdAsync(byte managerId);
        Task<List<EmployeeEnrolmentOverviewDTO>> GetEmployeesEnrolmentHistoryByIdAsync(byte userId);
        #endregion
    }
}
