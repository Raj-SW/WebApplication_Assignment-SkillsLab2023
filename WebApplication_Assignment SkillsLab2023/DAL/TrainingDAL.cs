using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class TrainingDAL : ITrainingDAL
    {
        //private readonly IDBCommand _idBCommand;

        public TrainingDAL()
        {
        }

        //public TrainingDAL(IDBCommand dBCommand) { this.idBCommand = dBCommand; }
        public List<TrainingModel> GetAllTrainingModels()
        {
            const string GET_ALL_TRAINING_QUERY = "SELECT * FROM [TrainingAssignment].[dbo].[Training]";
            DBCommand command = new DBCommand();
            var DataTable = command.GetData(GET_ALL_TRAINING_QUERY);
            List<TrainingModel>ListOfTrainingModels= new List<TrainingModel>();
            TrainingModel trainingModel;
            foreach (DataRow row in DataTable.Rows)
            {
                trainingModel = new TrainingModel();
                trainingModel.TrainingId = (int)row["TrainingId"];
                trainingModel.TrainingName = (string)row["TrainingName"];
                trainingModel.TrainingDescription = (string)row["TrainingDescription"];
                trainingModel.SeatsAvailable = (int)row["Seatsavailable"];
                trainingModel.TrainingRegistrationDeadline = (DateTime)row["TrainingRegistrationDeadline"];
                trainingModel.TrainingStatus = (string)row["TrainingStatus"];
                ListOfTrainingModels.Add(trainingModel);
            }
            return ListOfTrainingModels;
        }
        public List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId) 
        {
            const string GET_PREREQUISITE_BY_TRAINING_ID = "SELECT * FROM  [TrainingPrerequisite] WHERE TrainingId = @TrainingId";
            DBCommand command = new DBCommand();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));
            var DataTable = command.GetDataWithConditions(GET_PREREQUISITE_BY_TRAINING_ID,parameters);
            List<TrainingPrerequisiteModel> ListOfTrainingPrerequisiteModelsByTrainingId = new List<TrainingPrerequisiteModel>();
            TrainingPrerequisiteModel trainingPrerequisiteModel;
            foreach (DataRow row in DataTable.Rows)
            { 
                trainingPrerequisiteModel = new TrainingPrerequisiteModel();
                trainingPrerequisiteModel.PrerequisiteId = (int)row["PrerequisiteId"];
                trainingPrerequisiteModel.TrainingId = (int)row["TrainingId"];
                trainingPrerequisiteModel.PrerequisiteDescription = (string)row["PrerequisiteDescription"];
                ListOfTrainingPrerequisiteModelsByTrainingId.Add(trainingPrerequisiteModel);
            }
                return ListOfTrainingPrerequisiteModelsByTrainingId;
        }
        public bool EnrolEmployeeIntoTraining()
        {
            return true;
        }

        public int InsertIntoEnrolmentTable(int userId, int trainingId)
        {
            const string ENROLL_USER_RETRIEVE_ENROLMENT_ID_SCALAR_QUERY=
                  @"INSERT INTO Enrolment (UserId, TrainingId)
                    VALUES (@UserId, @TrainingId)
                    SELECT SCOPE_IDENTITY();";
            DBCommand command = new DBCommand();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", userId));
            parameters.Add(new SqlParameter("@TrainingId", trainingId));
            var obj = command.ExecuteScalar(ENROLL_USER_RETRIEVE_ENROLMENT_ID_SCALAR_QUERY, parameters);
            return Convert.ToInt32(obj);
        }

        public bool InsertIntoEnrolmentPrerequisiteTable(int enrolmentId, string filepath)
        {
            const string INSERT_ATTACHMENT_DETAILS_INTO_TABLE =
                  @"INSERT INTO EnrolmentPrerequisite (EnrolmentId,FilePath)
                    VALUES (@EnrolmentId, @FilePath);";
            DBCommand command = new DBCommand();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EnrolmentId", enrolmentId));
            parameters.Add(new SqlParameter("@FilePath", filepath));
            command.InsertUpdateData(INSERT_ATTACHMENT_DETAILS_INTO_TABLE, parameters);
            return true;
        }
    }
}