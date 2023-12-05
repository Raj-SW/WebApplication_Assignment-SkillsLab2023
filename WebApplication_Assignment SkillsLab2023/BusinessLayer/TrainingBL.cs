using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class TrainingBL : ITrainingBL
    {
        private ITrainingDAL _itrainingDAL;
        public TrainingBL(ITrainingDAL trainingDAL) { this._itrainingDAL = trainingDAL; }
        public List<TrainingModel> GetAllTraining()
        {
            TrainingDAL dal =new TrainingDAL();
            return dal.GetAllTrainingModels();
        }
    }
}