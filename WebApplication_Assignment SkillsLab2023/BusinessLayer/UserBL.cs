using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class UserBL : IUserBL
    {
        private readonly IUserDAL _userDAL;

        public UserBL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        #region Get Model 
        public async Task<string> GetEmployeeEmailbyUserIdAsync(byte UserId)
        {
            return await _userDAL.GetEmployeeEmailbyUserIdAsync(UserId);
        }

        public async Task<string> GetManagerEmailThroughEmployeeUserIdAsync(byte UserId)
        {
            return await _userDAL.GetManagerEmailThroughEmployeeUserIdAsync(UserId);
        }

        public async Task<string> GetUserNamebyUserIdAsync(byte UserId)
        {
            return await _userDAL.GetUserNamebyUserIdAsync(UserId);
        }
        #endregion

        #region User Model Manipulations
        public async Task<bool> ActivatePendingUserAsync(ActivationDTO activationDTO)
        {
            return await _userDAL.ActivatePendingUserAsync(activationDTO);
        }

        public async Task<List<UserModel>> GetAllPendingUserModelsAsync()
        {
            return await _userDAL.GetAllPendingUserModelsAsync();
        }

        public async Task DeactivatePendingUserAsync(byte UserID)
        {
            await _userDAL.DeactivatePendingUserAsync(UserID);
        }

        public async Task<bool> UpdateUserAndRolesAsync(UserAndRolesDTO userAndRolesDTO)
        {
            return await _userDAL.UpdateUserAndRolesAsync(userAndRolesDTO);
        }

        public async Task<List<UserAndRolesDTO>> GetAllUsersAndTheirRolesAsync()
        {
            return await _userDAL.GetAllUsersAndTheirRolesAsync();
        }
        #endregion

        #region Manager Manipulations
        public async Task<List<ManagerDTO>> GetAllManagersAsync()
        {
            return await _userDAL.GetAllManagersAsync();
        }

        public async Task<List<ManagerDTO>> GetAllManagersByDepartmentIdAsync(byte DepartmentId)
        {
            return await _userDAL.GetAllManagersByDepartmentIdAsync(DepartmentId);
        }
        #endregion

        #region User Roles Manipulations
        public async Task<List<RoleModel>> GetAllUserRolesAsync()
        {
            return await _userDAL.GetAllUserRolesAsync();
        }

        public async Task<List<UserRolesModel>> GetAllUserRolesModelByUserIdAsync(int UserId)
        {
            return await _userDAL.GetAllUserRolesModelByUserIdAsync(UserId);
        }
        #endregion

        #region EmployeeTraining
        public async Task AssignTrainingToEmployeeAsync(byte EmployeeId, byte TrainingId)
        {
            await _userDAL.AssignTrainingToEmployeeAsync(EmployeeId, TrainingId);
        }
        #endregion
    }
}
