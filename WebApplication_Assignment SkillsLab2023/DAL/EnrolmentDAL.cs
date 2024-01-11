using System;
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
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ManagerId", managerId));
            var result = await _command.GetDataWithConditionsAsync<GetPendingEmployeesEnrolmentOfAMangerDTO>(GET_EMPLOYEES_PENDING_ENROLMENT_BY_MANAGER_ID_QUERY, parameters);
            return result;
        }
        public async Task<List<UserPrerequisiteModel>> GetEnrolmentPrerequisitesOfAUserByEnrolmentIdAsync(byte enrolmentId)
        {
            const string GET_ENROLMENT_PREREQUISITES_OF_A_USER_BY_ENROLMENT_ID = @"
            SELECT ep.*
            FROM EnrolmentPrerequisite ep
            INNER JOIN Enrolment e ON e.EnrolmentId = ep.EnrolmentId
            WHERE ep.EnrolmentId = @EnrolmentId;";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@EnrolmentId", enrolmentId) };
            var result = await _command.GetDataWithConditionsAsync<UserPrerequisiteModel>(GET_ENROLMENT_PREREQUISITES_OF_A_USER_BY_ENROLMENT_ID, parameters);
            return result;
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
            return  await _command.IsRowExistsAsync(CHECK_IF_USER_ALREADY_REGISTERED, parameters);
        }
        #endregion

        #region Insert
        public async Task<bool> EnrolEmployeeIntoTrainingAsync(byte userId, byte trainingId, List<string> filepath)
        {
            string INSERT_ENROLMENT_QUERY =@"
            DECLARE @EnrolmentId INT;
            INSERT INTO Enrolment (UserId, TrainingId)
            VALUES (@UserId, @TrainingId);";
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
            return await _command.InsertUpdateDataAsync(INSERT_ENROLMENT_QUERY, parameters);
        }
        #endregion

        #region Update Enrolment
        public async Task<bool> ManagerApproveEnrolmentAsync(byte enrolmentId)
        {
            const string APPROVE_ENROLMENT_BY_ID_QUERY = @"UPDATE Enrolment SET ManagerApproval = 'Approved' WHERE EnrolmentId = @EnrolmentId";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@EnrolmentId", enrolmentId) };
            return await _command.InsertUpdateDataAsync(APPROVE_ENROLMENT_BY_ID_QUERY, parameters);
        }
        public async Task<bool> ManagerRejectEnrolmentAsync(byte enrolmentId, string remarks)
        {
            const string REJECT_ENROLMENT_BY_ID_QUERY = @"UPDATE Enrolment SET ManagerApproval = 'Rejected', Remarks = @Remarks WHERE EnrolmentId = @EnrolmentId";
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@EnrolmentId", enrolmentId),
                new SqlParameter("@Remarks", remarks),
            };
            return await _command.InsertUpdateDataAsync(REJECT_ENROLMENT_BY_ID_QUERY, parameters);
        }
        public Task AutomaticEnrolmentProcessingForTrainingByTrainingIdAsync(byte trainingId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<AutomaticProcessingDTO>> AutomaticEnrolmentProcessingForAllTrainingAsync()
        {
            const string EXECUTE_AUTOMATIC_ENROLMENT_PROCESSING_PROC = "EXEC AutomaticEnrolmentProcessingProcedure;";
            var result = await _command.GetDataAsync<AutomaticProcessingDTO>(EXECUTE_AUTOMATIC_ENROLMENT_PROCESSING_PROC);
            return result;
        }
        #endregion
    }
}