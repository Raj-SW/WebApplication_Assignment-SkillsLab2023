using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface IUserDAL
    {
        #region Get Model
        Task<List<UserAndRolesDTO>> GetAllUsersAndTheirRolesAsync();
        Task<string> GetEmployeeEmailbyUserIdAsync(byte UserId);
        Task<string> GetManagerEmailThroughEmployeeUserIdAsync(byte UserId);
        Task<List<UserRolesModel>> GetAllUserRolesModelByUserIdAsync(int UserId);
        Task<List<UserModel>> GetAllPendingUserModelsAsync();
        Task<List<RoleModel>> GetAllUserRolesAsync();
        Task<List<ManagerDTO>> GetAllManagersAsync();
        Task<List<ManagerDTO>> GetAllManagersByDepartmentIdAsync(byte DepartmentId);
        #endregion

        Task<bool> ActivatePendingUserAsync(ActivationDTO activationDTO);
        Task<bool> UpdateUserAndRolesAsync(UserAndRolesDTO userAndRolesDTO);
        Task DeactivatePendingUserAsync(byte UserID);
        Task AssignTrainingToEmployeeAsync(byte EmployeeId, byte TrainingId);
        Task<string> GetUserNamebyUserIdAsync(byte userId);
    }
}
