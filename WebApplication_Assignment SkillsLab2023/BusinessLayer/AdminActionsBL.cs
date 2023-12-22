using System.Collections.Generic;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL;
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
        public void ActivatePendingUser(byte UserID)
        {
            throw new System.NotImplementedException();
        }
        public void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId)
        {
            throw new System.NotImplementedException();
        }
        public void DeactivatePendingUser(byte UserID)
        {
            throw new System.NotImplementedException();
        }
        public void DeleteEmployeeEnrolment(byte Employee, byte TrainingId)
        {
            throw new System.NotImplementedException();
        }
        public List<UserModel> GetAllPendingUserModels()
        {

            return _adminActionsDAL.GetAllPendingUserModels();
        }
        public void PromoteUser(byte UserID)
        {
            throw new System.NotImplementedException();
        }
    }
}