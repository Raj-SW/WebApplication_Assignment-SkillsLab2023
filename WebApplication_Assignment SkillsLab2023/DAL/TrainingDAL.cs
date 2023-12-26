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
        private readonly IDBCommand _command;
        public TrainingDAL(IDBCommand command)
        {
            _command = command;
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
                trainingModel.TrainingId = (byte)row["TrainingId"];
                trainingModel.TrainingName = (string)row["TrainingName"];
                trainingModel.TrainingDescription = (string)row["TrainingDescription"];
                trainingModel.TrainingRegistrationDeadline = (DateTime)row["TrainingRegistrationDeadline"];
                trainingModel.TrainingStatus = (string)row["TrainingStatus"];
                trainingModel.SeatsTotal = (byte)row["SeatsTotal"];
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
                trainingPrerequisiteModel.PrerequisiteId = (byte)row["PrerequisiteId"];
                trainingPrerequisiteModel.TrainingId = (byte)row["TrainingId"];
                ListOfTrainingPrerequisiteModelsByTrainingId.Add(trainingPrerequisiteModel);
            }
                return ListOfTrainingPrerequisiteModelsByTrainingId;
        }
        public bool EnrolEmployeeIntoTraining(int userId, int trainingId, List<string> filepath)
        {
            string INSERT_ENROLMENT_QUERY =
            @"
            DECLARE @EnrolmentId INT;
            INSERT INTO Enrolment (UserId, TrainingId)
            VALUES (@UserId, @TrainingId);
            SET @EnrolmentId = SCOPE_IDENTITY();
            INSERT INTO EnrolmentPrerequisite (EnrolmentId, FilePath)
            VALUES ";
            DBCommand command = new DBCommand();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@TrainingId", trainingId)
            };
            foreach (string filePathEntry in filepath)
            {   
                int index=filepath.IndexOf(filePathEntry);
                INSERT_ENROLMENT_QUERY += $"(@EnrolmentId, @FilePath{index}),";
                parameters.Add(new SqlParameter($"@FilePath{index}", filePathEntry));
            }
            INSERT_ENROLMENT_QUERY = INSERT_ENROLMENT_QUERY.TrimEnd(',') + ";";
            command.InsertUpdateData(INSERT_ENROLMENT_QUERY , parameters);
            return true;
        }
        public List<PrerequisitesModel> GetAllPrerequisites()
        {
            const string GET_ALL_PREREQUISITES_QUERY = "SELECT * FROM  [Prerequisites]";
            List<PrerequisitesModel> prerequisitesModelsList = new List<PrerequisitesModel>();
            PrerequisitesModel prerequisitesModel;
            var dt = _command.GetData(GET_ALL_PREREQUISITES_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                prerequisitesModel = new PrerequisitesModel();
                prerequisitesModel.PrerequisiteId = (byte)row["PrerequisiteId"];
                prerequisitesModel.PrerequisiteDescription = (string)row["PrerequisiteDescription"];
                prerequisitesModelsList.Add(prerequisitesModel);
            }
            return prerequisitesModelsList;
        }
    }
}