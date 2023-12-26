using Microsoft.Ajax.Utilities;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class AdminActionsBL : IAdminActionsBL
    {
        private readonly IAdminActionsDAL _adminActionsDAL;
        public AdminActionsBL(IAdminActionsDAL adminActionsDAL) 
        {
            _adminActionsDAL = adminActionsDAL;
        }
        public bool ActivatePendingUser(ActivationDTO activationDTO)
        {
            return _adminActionsDAL.ActivatePendingUser(activationDTO);
        }
        public void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId)
        {
            throw new System.NotImplementedException();
        }
        public bool CreateTraining(CreateTrainingDTO createTrainingDTO)
        {

            return _adminActionsDAL.CreateTraining(createTrainingDTO);
        }

        public void DeactivatePendingUser(byte UserID)
        {
            throw new System.NotImplementedException();
        }
        public void DeleteEmployeeEnrolment(byte Employee, byte TrainingId)
        {
            throw new System.NotImplementedException();
        }
        public List<DepartmentModel> GetAllDepartments()
        {
            return _adminActionsDAL.GetAllDepartments();
        }
        public List<ManagerDTO> GetAllManagers()
        {
            return _adminActionsDAL.GetAllManagers();
        }
        public List<ManagerDTO> GetAllManagersByDepartmentId(byte DepartmentId)
        {
          return  _adminActionsDAL.GetAllManagersByDepartmentId(DepartmentId);
        }
        public List<UserModel> GetAllPendingUserModels()
        {

            return _adminActionsDAL.GetAllPendingUserModels();
        }
        public List<RoleModel> GetAllUserRoles()
        {
            return _adminActionsDAL.GetAllUserRoles();
        }
        public void PromoteUser(byte UserID)
        {
            throw new System.NotImplementedException();
        }
    }
}