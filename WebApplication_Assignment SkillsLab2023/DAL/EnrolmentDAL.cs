using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        public List<GetPendingEmployeesEnrolmentOfAMangerDTO> GetEmployeesPendingEnrolmentByManagerId(byte managerId)
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
            var dt = _command.GetDataWithConditions(GET_EMPLOYEES_PENDING_ENROLMENT_BY_MANAGER_ID_QUERY, parameters);
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
        public List<UserPrerequisiteModel> GetEnrolmentPrerequisitesOfAUserByEnrolmentId(byte enrolmentId)
        {
            const string GET_ENROLMENT_PREREQUISITES_OF_A_USER_BY_ENROLMENT_ID = @"
            SELECT ep.*
            FROM EnrolmentPrerequisite ep
            INNER JOIN Enrolment e ON e.EnrolmentId = ep.EnrolmentId
            WHERE ep.EnrolmentId = @EnrolmentId;";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@EnrolmentId", enrolmentId) };
            UserPrerequisiteModel userPrerequisiteModel;
            List<UserPrerequisiteModel> userPrerequisiteModelList = new List<UserPrerequisiteModel>();
            var dt = _command.GetDataWithConditions(GET_ENROLMENT_PREREQUISITES_OF_A_USER_BY_ENROLMENT_ID, parameters);
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
        public List<UserPrerequisiteModel> GetAllEnrolmentsManagerWise(byte ManagerId)
        {
            throw new NotImplementedException();
        }
        public bool isUserAlreadyRegisteredInTraining(byte trainingId, byte UserId)
        {
            const string CHECK_IF_USER_ALREADY_REGISTERED = @"SELECT TOP(1) * FROM Enrolment WHERE TrainingId = @TrainingId AND UserId = @UserId AND RegistrationStatus = 'Pending' AND ManagerApproval = 'Pending';";
            List<SqlParameter> parameters = new List<SqlParameter>() { 
                new SqlParameter("@TrainingId",trainingId),
                new SqlParameter("@UserId",UserId)
            };
            var dt = _command.GetDataWithConditions(CHECK_IF_USER_ALREADY_REGISTERED, parameters);
            return dt.Rows.Count>0;
        }
        #endregion

        #region Insert
        public bool EnrolEmployeeIntoTraining(byte userId, byte trainingId, List<string> filepath)
        {
            string INSERT_ENROLMENT_QUERY =
           @"
            DECLARE @EnrolmentId INT;
            INSERT INTO Enrolment (UserId, TrainingId)
            VALUES (@UserId, @TrainingId);
            
             ";
            DBCommand command = new DBCommand();
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
            command.InsertUpdateData(INSERT_ENROLMENT_QUERY, parameters);
            return true;
        }
        #endregion

        #region Update Enrolment
        public bool ManagerApproveEnrolmentAsync(byte enrolmentId)
        {
            const string APPROVE_ENROLMENT_BY_ID_QUERY = @"UPDATE Enrolment SET ManagerApproval = 'Approved' WHERE EnrolmentId = @EnrolmentId";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@EnrolmentId", enrolmentId) };
            _command.InsertUpdateData(APPROVE_ENROLMENT_BY_ID_QUERY, parameters);
            return true;
        }
        public bool ManagerRejectEnrolmentAsync(byte enrolmentId, string remarks)
        {
            const string REJECT_ENROLMENT_BY_ID_QUERY = @"UPDATE Enrolment SET ManagerApproval = 'Rejected', Remarks = @Remarks WHERE EnrolmentId = @EnrolmentId";
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@EnrolmentId", enrolmentId),
                new SqlParameter("@Remarks", remarks),
            };
            _command.InsertUpdateData(REJECT_ENROLMENT_BY_ID_QUERY, parameters);
            return true;
        }
        #endregion
    }
}