﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.DAL.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class EnrolmentDAL : IEnrolmentDAL
    {
        private readonly IDBCommand _command;
        public EnrolmentDAL(IDBCommand command)
        {
            _command = command;
        }

        #region Get Model
        public async Task<List<GetPendingEmployeesEnrolmentOfAMangerDTO>> GetEmployeesPendingEnrolmentByManagerIdAsync(byte managerId)
        {
            const string GET_EMPLOYEES_PENDING_ENROLMENT_BY_MANAGER_ID_QUERY = @"
            SELECT e.EnrolmentId, e.TrainingId, e.UserId, u.UserFirstName, u.UserLastName, e.EnrolmentDateTime, t.TrainingName,t.TrainingRegistrationDeadline
            FROM Enrolment e 
            INNER JOIN [User] u ON u.UserId = e.UserId
            INNER JOIN Training t ON t.TrainingId = e.TrainingId
            WHERE e.ManagerApproval = 'Pending' AND u.ManagerId = @ManagerId";

            List<GetPendingEmployeesEnrolmentOfAMangerDTO> employeesEnrolmentList = new List<GetPendingEmployeesEnrolmentOfAMangerDTO>();
            GetPendingEmployeesEnrolmentOfAMangerDTO employeesEnrolment;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ManagerId", managerId));
            var dt = await _command.GetDataWithConditionsAsync(GET_EMPLOYEES_PENDING_ENROLMENT_BY_MANAGER_ID_QUERY, parameters);
            foreach (DataRow row in dt.Rows)
            {
                employeesEnrolment = new GetPendingEmployeesEnrolmentOfAMangerDTO();
                employeesEnrolment.EnrolmentId = (byte)row["EnrolmentId"];
                employeesEnrolment.TrainingId = (byte)row["TrainingId"];
                employeesEnrolment.UserId = (byte)row["UserId"];
                employeesEnrolment.UserFirstName = (string)row["UserFirstName"];
                employeesEnrolment.UserLastName = (string)row["UserLastName"];
                employeesEnrolment.EnrolmentDateTime = (DateTime)row["EnrolmentDateTime"];
                employeesEnrolment.TrainingName = (string)row["TrainingName"];
                employeesEnrolment.TrainingRegistrationDeadline = (DateTime)row["TrainingRegistrationDeadline"];
                employeesEnrolmentList.Add(employeesEnrolment);
            }
            return employeesEnrolmentList;
        }
        public async Task<List<UserPrerequisiteModel>> GetEnrolmentPrerequisitesOfAUserByEnrolmentIdAsync(byte enrolmentId)
        {
            const string GET_ENROLMENT_PREREQUISITES_OF_A_USER_BY_ENROLMENT_ID = @"
            SELECT ep.*
            FROM EnrolmentPrerequisite ep
            INNER JOIN Enrolment e ON e.EnrolmentId = ep.EnrolmentId
            WHERE ep.EnrolmentId = @EnrolmentId;";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@EnrolmentId", enrolmentId) };
            UserPrerequisiteModel userPrerequisiteModel;
            List<UserPrerequisiteModel> userPrerequisiteModelList = new List<UserPrerequisiteModel>();
            var dt = await _command.GetDataWithConditionsAsync(GET_ENROLMENT_PREREQUISITES_OF_A_USER_BY_ENROLMENT_ID, parameters);
            foreach (DataRow item in dt.Rows)
            {
                userPrerequisiteModel = new UserPrerequisiteModel();
                userPrerequisiteModel.EnrolmentPrerequisiteId = (byte)item["EnrolmentPrerequisiteId"];
                userPrerequisiteModel.EnrolmentId = (byte)item["EnrolmentId"];
                userPrerequisiteModel.FilePath = (string)item["FilePath"];
                userPrerequisiteModelList.Add(userPrerequisiteModel);
            }
            return userPrerequisiteModelList;
        }
        public Task<List<UserPrerequisiteModel>> GetAllEnrolmentsManagerWiseAsync(byte ManagerId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> isUserAlreadyRegisteredInTrainingAsync(byte trainingId, byte UserId)
        {
            const string CHECK_IF_USER_ALREADY_REGISTERED = @"SELECT TOP(1) * FROM Enrolment WHERE TrainingId = @TrainingId AND UserId = @UserId AND RegistrationStatus = 'Pending' AND ManagerApproval = 'Pending';";
            List<SqlParameter> parameters = new List<SqlParameter>() { 
                new SqlParameter("@TrainingId",trainingId),
                new SqlParameter("@UserId",UserId)
            };
            var dt = await _command.GetDataWithConditionsAsync(CHECK_IF_USER_ALREADY_REGISTERED, parameters);
            return dt.Rows.Count>0;
        }
        #endregion

        #region Insert
        public async Task<bool> EnrolEmployeeIntoTrainingAsync(byte userId, byte trainingId, List<string> filepath)
        {
            string INSERT_ENROLMENT_QUERY =
           @"
            DECLARE @EnrolmentId INT;
            INSERT INTO Enrolment (UserId, TrainingId)
            VALUES (@UserId, @TrainingId);
            
             ";
           
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TrainingId", trainingId)
            };
            int index = 0;
            if(filepath != null && filepath.Count > 0)
            {
                INSERT_ENROLMENT_QUERY += "SET @EnrolmentId = SCOPE_IDENTITY();" +
                    "INSERT INTO EnrolmentPrerequisite (EnrolmentId, FilePath) VALUES";
                foreach (string filePathEntry in filepath)
                {
                    INSERT_ENROLMENT_QUERY += $"(@EnrolmentId, @FilePath{index}),";
                    parameters.Add(new SqlParameter($"@FilePath{index}", filePathEntry));
                    index++;
                }
                INSERT_ENROLMENT_QUERY = INSERT_ENROLMENT_QUERY.TrimEnd(',') + ";";
            }
            await _command.InsertUpdateDataAsync(INSERT_ENROLMENT_QUERY, parameters);
            return true;
        }
        #endregion

        #region Update Enrolment
        public async Task<bool> ManagerApproveEnrolmentAsync(byte enrolmentId)
        {
            const string APPROVE_ENROLMENT_BY_ID_QUERY = @"UPDATE Enrolment SET ManagerApproval = 'Approved' WHERE EnrolmentId = @EnrolmentId";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@EnrolmentId", enrolmentId) };
            await _command.InsertUpdateDataAsync(APPROVE_ENROLMENT_BY_ID_QUERY, parameters);
            return true;
        }
        public async Task<bool> ManagerRejectEnrolmentAsync(byte enrolmentId, string remarks)
        {
            const string REJECT_ENROLMENT_BY_ID_QUERY = @"UPDATE Enrolment SET ManagerApproval = 'Rejected', Remarks = @Remarks WHERE EnrolmentId = @EnrolmentId";
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@EnrolmentId", enrolmentId),
                new SqlParameter("@Remarks", remarks),
            };
            await _command.InsertUpdateDataAsync(REJECT_ENROLMENT_BY_ID_QUERY, parameters);
            return true;
        }
        public Task AutomaticEnrolmentProcessingForTrainingByTrainingIdAsync(byte trainingId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<AutomaticProcessingDTO>> AutomaticEnrolmentProcessingForAllTrainingAsync()
        {
            const string EXECUTE_AUTOMATIC_ENROLMENT_PROCESSING_PROC = "EXEC AutomaticEnrolmentProcessingProcedure;";
            List<AutomaticProcessingDTO> listofDTO = new List<AutomaticProcessingDTO>();
            AutomaticProcessingDTO dto;
            var dt = await _command.GetDataAsync(EXECUTE_AUTOMATIC_ENROLMENT_PROCESSING_PROC);
            foreach( DataRow row in dt.Rows )
            {
                dto = new AutomaticProcessingDTO();
                dto.TrainingId = (byte)row["TrainingId"];
                dto.TrainingName = (string)row["TrainingName"];
                dto.EnrolmentId = (byte)row["EnrolmentId"];
                dto.UserId = (byte)row["UserId"];
                dto.UserFirstName = (string)row["UserFirstName"];
                dto.UserLastName = (string)row["UserLastName"];
                dto.Email = (string)row["Email"];
                listofDTO.Add(dto);
            }
            return listofDTO;
        }
        #endregion
    }
}