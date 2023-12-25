﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface IAdminActionsDAL
    {
        List<UserModel> GetAllPendingUserModels();
        bool ActivatePendingUser(ActivationDTO activationDTO);
        void DeactivatePendingUser(byte UserID);
        void PromoteUser(byte UserID);
        void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId);
        void DeleteEmployeeEnrolment(byte Employee, byte TrainingId);
        List<RoleModel> GetAllUserRoles();
        List<ManagerDTO> GetAllManagers();
        List<ManagerDTO> GetAllManagersByDepartmentId(byte DepartmentId);
        List<DepartmentModel> GetAllDepartments();
    }
}