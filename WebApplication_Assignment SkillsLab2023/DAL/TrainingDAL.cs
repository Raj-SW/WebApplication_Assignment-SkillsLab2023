using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class TrainingDAL : ITrainingDAL
    {
        private readonly IDBCommand idBCommand;

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
                trainingModel.TrainingName = (string)row["TrainingName"];
                trainingModel.TrainingDescription = (string)row["TrainingDescription"];
                trainingModel.SeatsAvailable = (int)row["Seatsavailable"];
                trainingModel.TrainingRegistrationDeadline = (DateTime)row["TrainingRegistrationDeadline"];
                trainingModel.TrainingStatus = (string)row["TrainingStatus"];
                ListOfTrainingModels.Add(trainingModel);
            }
            return ListOfTrainingModels;
        }
    }
}