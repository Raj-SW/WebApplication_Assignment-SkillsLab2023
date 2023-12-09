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
        private readonly ITrainingDAL _itrainingDAL;
        public TrainingBL(ITrainingDAL trainingDAL)
        {
            _itrainingDAL = trainingDAL;
        }
        public List<TrainingModel> GetAllTraining()
        {
            
            return _itrainingDAL.GetAllTrainingModels();
        }
        public List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId)
        {
            return _itrainingDAL.GetTrainingPrerequisitesById(trainingId);
        }
    }
}