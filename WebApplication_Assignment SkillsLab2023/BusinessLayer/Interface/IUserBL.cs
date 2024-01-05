using System.Collections.Generic;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface IUserBL
    {
        #region Get
        List<UserAndRolesDTO> GetAllUsersAndTheirRoles();
        string GetEmployeeEmailbyUserId(byte UserId);
        string GetManagerEmailThroughEmployeeUserId(byte UserId);
        List<UserRolesModel> GetAllUserRolesModelByUserId(int UserId);
        List<UserModel> GetAllPendingUserModels();
        List<RoleModel> GetAllUserRoles();
        List<ManagerDTO> GetAllManagers();
        List<ManagerDTO> GetAllManagersByDepartmentId(byte DepartmentId);
        #endregion

        bool UpdateUserAndRoles(UserAndRolesDTO userAndRolesDTO);
        bool ActivatePendingUser(ActivationDTO activationDTO);
        void DeactivatePendingUser(byte UserID);
        void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId);
        string GetUserNamebyUserId(byte userId);
    }
}