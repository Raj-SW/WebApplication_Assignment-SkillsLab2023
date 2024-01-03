using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        #region User Model Manipulations
        public bool ActivatePendingUser(ActivationDTO activationDTO)
        {
            return _userDAL.ActivatePendingUser(activationDTO);
        }
        public List<UserModel> GetAllPendingUserModels()
        {
            return _userDAL.GetAllPendingUserModels();
        }
        public void DeactivatePendingUser(byte UserID)
        {
            throw new NotImplementedException();
        }
        public bool UpdateUserAndRoles(UserAndRolesDTO userAndRolesDTO)
        {
            return _userDAL.UpdateUserAndRoles(userAndRolesDTO);
        }
        public List<UserAndRolesDTO> GetAllUsersAndTheirRoles()
        {
            return _userDAL.GetAllUsersAndTheirRoles();
        }
        #endregion

        #region Manager Manipulations
        public List<ManagerDTO> GetAllManagers()
        {
            return _userDAL.GetAllManagers();
        }
        public List<ManagerDTO> GetAllManagersByDepartmentId(byte DepartmentId)
        {
            return _userDAL.GetAllManagersByDepartmentId(DepartmentId);
        }
        #endregion

        #region User Roles Manipulations
        public List<RoleModel> GetAllUserRoles()
        {
            return _userDAL.GetAllUserRoles();
        }
        public List<UserRolesModel> GetAllUserRolesModelByUserId(int UserId)
        {
            return _userDAL.GetAllUserRolesModelByUserId(UserId);
        }
        #endregion

        #region EmployeeTraining
        public void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId)
        {
            throw new NotImplementedException();
        }

        
        #endregion

    }
}