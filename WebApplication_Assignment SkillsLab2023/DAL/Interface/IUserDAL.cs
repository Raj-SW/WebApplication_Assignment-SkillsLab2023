using System.Collections.Generic;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface IUserDAL
    {
        #region Get Model
        List<UserAndRolesDTO> GetAllUsersAndTheirRoles();
        #endregion
        List<UserRolesModel> GetAllUserRolesModelByUserId(int UserId);
        List<UserModel> GetAllPendingUserModels();
        bool ActivatePendingUser(ActivationDTO activationDTO);
        bool UpdateUserAndRoles(UserAndRolesDTO userAndRolesDTO);
        void DeactivatePendingUser(byte UserID);
        List<RoleModel> GetAllUserRoles();
        List<ManagerDTO> GetAllManagers();
        List<ManagerDTO> GetAllManagersByDepartmentId(byte DepartmentId);
        void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId);
    }

}