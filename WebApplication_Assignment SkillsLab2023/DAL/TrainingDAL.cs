﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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

        #region Get Model
        public async Task<List<TrainingWithPrerequisitesModel>> GetAllTrainingModelsAsync()
        {
            const string GET_ALL_TRAINING_QUERY = "SELECT *  FROM [TrainingAssignment].[dbo].[Training]";
            var DataTable = await _command.GetDataAsync(GET_ALL_TRAINING_QUERY);
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
        public async Task<List<PrerequisitesModel>> GetTrainingPrerequisitesByIdAsync(int trainingId) 
        {
            const string GET_PREREQUISITE_BY_TRAINING_ID = "SELECT p.* FROM  [Prerequisites] p INNER JOIN [TrainingPrerequisite] tp ON p.PrerequisiteId = tp.PrerequisiteId WHERE tp.TrainingId = @TrainingId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));
            var DataTable = await _command.GetDataWithConditionsAsync(GET_PREREQUISITE_BY_TRAINING_ID,parameters);
            List<PrerequisitesModel> ListOfTrainingPrerequisiteModelsByTrainingId = new List<PrerequisitesModel>();
            PrerequisitesModel trainingPrerequisiteModel;
            foreach (DataRow row in DataTable.Rows)
            { 
                trainingPrerequisiteModel = new PrerequisitesModel();
                trainingPrerequisiteModel.PrerequisiteId = (byte)row["PrerequisiteId"];
                trainingPrerequisiteModel.PrerequisiteDescription = (string)row["PrerequisiteDescription"];
                ListOfTrainingPrerequisiteModelsByTrainingId.Add(trainingPrerequisiteModel);
            }
                return ListOfTrainingPrerequisiteModelsByTrainingId;
        }
        public async Task<List<PrerequisitesModel>> GetAllPrerequisitesAsync()
        {
            const string GET_ALL_PREREQUISITES_QUERY = "SELECT * FROM  [Prerequisites]";
            List<PrerequisitesModel> prerequisitesModelsList = new List<PrerequisitesModel>();
            PrerequisitesModel prerequisitesModel;
            var dt = await _command.GetDataAsync(GET_ALL_PREREQUISITES_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                prerequisitesModel = new PrerequisitesModel();
                prerequisitesModel.PrerequisiteId = (byte)row["PrerequisiteId"];
                prerequisitesModel.PrerequisiteDescription = (string)row["PrerequisiteDescription"];
                prerequisitesModelsList.Add(prerequisitesModel);
            }
            return prerequisitesModelsList;
        }
        public async Task<List<PrerequisitesModel>> GetAllPrerequisiteOfATrainingModelByTrainingIdAsync(byte trainingId)
        {
            const string GET_ALL_PREREQUISITES_OF_A_TRAINING = @"SELECT p.* FROM Prerequisites p INNER JOIN TrainingPrerequisite tp ON p.PrerequisiteId= tp.PrerequisiteId WHERE tp.TrainingId = @TrainingId; ";
            List<PrerequisitesModel> listOfPrerequisite = new List<PrerequisitesModel>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            PrerequisitesModel prerequisitesModel;
            parameters.Add(new SqlParameter("@TrainingId",trainingId));
            var dt = await _command.GetDataWithConditionsAsync(GET_ALL_PREREQUISITES_OF_A_TRAINING,parameters);
            foreach (DataRow row in dt.Rows) 
            {
                prerequisitesModel = new PrerequisitesModel();
                prerequisitesModel.PrerequisiteId = (byte)row["PrerequisiteId"];    
                prerequisitesModel.PrerequisiteDescription = (string)row["PrerequisiteDescription"];
                listOfPrerequisite.Add(prerequisitesModel);
            }
            return listOfPrerequisite;
        }
        public async Task<string> GetTrainingNameByTrainingIdAsync(byte trainingId) 
        {
            const string GET_TRAINING_NAME_BY_TRAINING_ID_QUERY= @"SELECT TrainingName FROM Training WHERE TrainingId = @TrainingId";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("TrainingId",trainingId)};
            var dt = await _command.GetDataWithConditionsAsync(GET_TRAINING_NAME_BY_TRAINING_ID_QUERY,parameters);
            string TrainingName = (string)dt.Rows[0]["TrainingName"];
            return TrainingName;
        }
        #endregion

        #region Insert
        public async Task<bool> CreateTrainingAsync(CreateTrainingDTO createTrainingDTO)
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
            if (createTrainingDTO.Prerequisites != null && createTrainingDTO.Prerequisites.Count > 0)
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
            await _command.InsertUpdateDataAsync(CREATE_TRAINING_QUERY, parameters);
            return true;
        }
        public async Task<bool> AddPrerequisiteToTrainingAsync(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            //TODO: CHECK IF ROW EXIST
            const string ADD_PREREQUISITE_TO_TRAINING_QUERY = @"
                INSERT INTO [TrainingPrerequisite]
                VALUES(@PrerequisiteId,@TrainingId);";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PrerequisiteId",trainingPrerequisiteModel.PrerequisiteId));
            parameters.Add(new SqlParameter("@TrainingId",trainingPrerequisiteModel.TrainingId));
            await _command.InsertUpdateDataAsync(ADD_PREREQUISITE_TO_TRAINING_QUERY, parameters);
            return true;
        }
        public async Task<bool> CreatePrerequisiteAsync(string description)
        {
            const string INSERT_PREREQUISITE_QUERY = @"INSERT INTO Prerequisites (PrerequisiteDescription) VALUES (@PrerequisiteDescription);";
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@PrerequisiteDescription", description),
            };
            await _command.InsertUpdateDataAsync(INSERT_PREREQUISITE_QUERY, parameters);
            return true;
        }
        #endregion

        #region Update
        public async Task<bool> UpdateTrainingAsync(TrainingModel trainingmodel)
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
            await _command.InsertUpdateDataAsync(UPDATE_TRAINING_QUERY, parameters);
            return true;
        }
        public async Task<bool> UpdateTrainingPrerequisiteAsync(byte TrainingId, List<byte> Prerequisites)
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
            await _command.InsertUpdateDataAsync(UPDATE_TRAINING_PREQUISITE_QUERY, parameters);
            return true;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteTrainingAsync(byte trainingId)
        {
            const string DELETE_TRAINING_QUERY = @"DELETE From Training WHERE TrainingId = @TrainingId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingId", trainingId));
            await _command.InsertUpdateDataAsync(DELETE_TRAINING_QUERY, parameters);
            return true;
        }
        public async Task<bool> isTrainingDeletableAsync(byte trainingId)
        {
            const string SELECT_ENROLMENTS_QUERY = @"SELECT * FROM Enrolment WHERE TrainingId = @TrainingId;";
            List<SqlParameter> parameters = new List<SqlParameter>() {new SqlParameter("@TrainingId",trainingId) };
            var dt = await _command.GetDataWithConditionsAsync(SELECT_ENROLMENTS_QUERY, parameters);
            return dt.Rows.Count<=0;
        }
        #endregion
    }
}