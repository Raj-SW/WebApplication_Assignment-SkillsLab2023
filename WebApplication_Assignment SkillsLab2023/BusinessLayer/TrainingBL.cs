using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class TrainingBL : ITrainingBL
    {
        private readonly ITrainingDAL _itrainingDAL;
        private readonly IFileHandlerService _iFileHandlerService;

        public TrainingBL(ITrainingDAL trainingDAL, IFileHandlerService fileHandlerService)
        {
            _itrainingDAL = trainingDAL;
            _iFileHandlerService = fileHandlerService;
        }
        public List<TrainingModel> GetAllTraining()
        {
            
            return _itrainingDAL.GetAllTrainingModels();
        }
        public List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId)
        {
            return _itrainingDAL.GetTrainingPrerequisitesById(trainingId);
        }

        public bool EnrolEmployeeIntoTraining(int userId, int trainingId, HttpFileCollectionBase FileCollection)
        {
            TaskResult uploadTaskResult= new TaskResult();
            //TODO:
            //Security protocols here
            uploadTaskResult = _iFileHandlerService.FileUpload(userId,trainingId,FileCollection);
            if (uploadTaskResult.isSuccess)
            {
                var result = _itrainingDAL.EnrolEmployeeIntoTraining(userId,trainingId,uploadTaskResult.ResultMessageList);
            }
            return true;
        }
    }
}