using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
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
        public List<TrainingWithPrerequisitesModel> GetAllTrainingModels()
        {
            const string GET_ALL_TRAINING_QUERY = "SELECT * FROM [TrainingAssignment].[dbo].[Training]";
            DBCommand command = new DBCommand();
            var DataTable = command.GetData(GET_ALL_TRAINING_QUERY);
            List<TrainingWithPrerequisitesModel> ListOfTrainingModels= new List<TrainingWithPrerequisitesModel>();
            TrainingWithPrerequisitesModel trainingModel;
            foreach (DataRow row in DataTable.Rows)
            {
                trainingModel = new TrainingWithPrerequisitesModel();
                trainingModel.TrainingId = (byte)row["TrainingId"];
                trainingModel.TrainingName = (string)row["TrainingName"];
                trainingModel.TrainingDescription = (string)row["TrainingDescription"];
                trainingModel.TrainingRegistrationDeadline = (DateTime)row["TrainingRegistrationDeadline"];
                trainingModel.TrainingStatus = (string)row["TrainingStatus"];
                trainingModel.SeatsTotal = (byte)row["SeatsTotal"];
                trainingModel.CoachId = (byte)row["CoachId"];
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

        public bool CreateTraining(CreateTrainingDTO createTrainingDTO)
        {
            string CREATE_TRAINING_QUERY = @"INSERT INTO [Training] (TrainingName,TrainingStatus,DepartmentPriority,TrainingDescription,TrainingRegistrationDeadline,SeatsTotal,CoachId)
                                            VALUES (@TrainingName,@TrainingStatus,@DepartmentPriority,@TrainingDescription,CONVERT(DATE, @TrainingRegistrationDeadline),@TotalSeats,@CoachId);";
            var index = 0;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingName", createTrainingDTO.TrainingName));
            parameters.Add(new SqlParameter("@TrainingStatus", createTrainingDTO.TrainingStatus));
            parameters.Add(new SqlParameter("@DepartmentPriority", createTrainingDTO.DepartmentPriority));
            parameters.Add(new SqlParameter("@TrainingDescription", createTrainingDTO.TrainingDescription));
            parameters.Add(new SqlParameter("@TrainingRegistrationDeadline", createTrainingDTO.RegistrationDeadline));
            parameters.Add(new SqlParameter("@TotalSeats", createTrainingDTO.TotalSeats));
            parameters.Add(new SqlParameter("@CoachId", createTrainingDTO.Coach));
            if (createTrainingDTO.Prerequisites.Count > 0)
            {
                CREATE_TRAINING_QUERY +=
                    "DECLARE @generateTrainingId INT; SET @generateTrainingId = SCOPE_IDENTITY();";
                foreach (var prerequisite in createTrainingDTO.Prerequisites)
                {
                    CREATE_TRAINING_QUERY += $"INSERT INTO [TrainingPrerequisite](PrerequisiteId, TrainingId) VALUES(@PrerequisiteId{index},@generateTrainingId);";
                    parameters.Add(new SqlParameter($"@PrerequisiteID{index}", prerequisite));
                    index++;
                }
            }
            _command.InsertUpdateData(CREATE_TRAINING_QUERY, parameters);
            return true;
        }
        public bool UpdateTraining(TrainingModel trainingmodel)
        {
            const string UPDATE_TRAINING_QUERY = @"
                UPDATE Training
                SET
                    TrainingName = @UpdatedTrainingName,
                    TrainingStatus = @UpdatedStatus,
                    DepartmentPriority = @UpdatedPriority,
                    TrainingDescription = @UpdatedDescription,
                    SeatsTotal = @UpdatedSeatsTotal,
                    CoachId = @UpdatedCoachId,
                    TrainingRegistrationDeadline = @UpdatedDeadline
                WHERE
                    TrainingId = @TrainingId; ";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UpdatedTrainingName", trainingmodel.TrainingName));
            parameters.Add(new SqlParameter("@UpdatedStatus", trainingmodel.TrainingStatus));
            parameters.Add(new SqlParameter("@UpdatedPriority", trainingmodel.DepartmentPriority));
            parameters.Add(new SqlParameter("@UpdatedSeatsTotal", trainingmodel.SeatsTotal));
            parameters.Add(new SqlParameter("@UpdatedCoachId", trainingmodel.CoachId));
            parameters.Add(new SqlParameter("@UpdatedDeadline", trainingmodel.TrainingRegistrationDeadline));
            parameters.Add(new SqlParameter("@UpdatedDescription", trainingmodel.TrainingDescription));
            parameters.Add(new SqlParameter("@TrainingId", trainingmodel.TrainingId));
            _command.InsertUpdateData(UPDATE_TRAINING_QUERY, parameters);
            return true;
        }
        public bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            //TODO: CHECK IF ROW EXIST
            const string ADD_PREREQUISITE_TO_TRAINING_QUERY = @"
                INSERT INTO [TrainingPrerequisite]
                VALUES(@PrerequisiteId,@TrainingId);";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PrerequisiteId",trainingPrerequisiteModel.PrerequisiteId));
            parameters.Add(new SqlParameter("@TrainingId",trainingPrerequisiteModel.TrainingId));
            _command.InsertUpdateData(ADD_PREREQUISITE_TO_TRAINING_QUERY, parameters);
            return true;
        }
        public bool UpdateTrainingPrerequisite(byte TrainingId, List<byte> Prerequisites)
        {
            string UPDATE_TRAINING_PREQUISITE_QUERY = @"
                DELETE FROM TrainingPrerequisite WHERE TrainingId=@TrainingId;";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", TrainingId));
            var index = 0;
            foreach (var prerequisite in Prerequisites)
            {
                UPDATE_TRAINING_PREQUISITE_QUERY += $"INSERT INTO TrainingPrerequisite (TrainingId,PrerequisiteId) VALUES (@TrainingId,@PrerequisiteId{index});";
                parameters.Add(new SqlParameter($"@PrerequisiteId{index}", prerequisite));
                index++;
            }
            _command.InsertUpdateData(UPDATE_TRAINING_PREQUISITE_QUERY, parameters);
            return true;
        }
        public bool DeleteTraining(byte trainingId)
        {
            const string DELETE_TRAINING_QUERY = @"UPDATE Training SET isDeleted = 1 WHERE TrainingId = @TrainingId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));
            _command.InsertUpdateData(DELETE_TRAINING_QUERY, parameters);
            return true;
        }
        public List<PrerequisitesModel> GetAllPrerequisiteOfATrainingModelByTrainingId(byte trainingId)
        {
            const string GET_ALL_PREREQUISITES_OF_A_TRAINING = @"SELECT p.* FROM Prerequisites p INNER JOIN TrainingPrerequisite tp ON p.PrerequisiteId= tp.PrerequisiteId WHERE tp.TrainingId = @TrainingId; ";
            List<PrerequisitesModel> listOfPrerequisite = new List<PrerequisitesModel>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            PrerequisitesModel prerequisitesModel;
            parameters.Add(new SqlParameter("@TrainingId",trainingId));
            var dt = _command.GetDataWithConditions(GET_ALL_PREREQUISITES_OF_A_TRAINING,parameters);
            foreach (DataRow row in dt.Rows) 
            {
                prerequisitesModel = new PrerequisitesModel();
                prerequisitesModel.PrerequisiteId = (byte)row["PrerequisiteId"];    
                prerequisitesModel.PrerequisiteDescription = (string)row["PrerequisiteDescription"];
                listOfPrerequisite.Add(prerequisitesModel);
            }
            return listOfPrerequisite;
        }
    }
}