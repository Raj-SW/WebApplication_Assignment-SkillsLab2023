using ConsoleApp5.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
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
        public List<PrerequisitesModel> GetAllPrerequisites()
        {
            return _itrainingDAL.GetAllPrerequisites();
        }
        public bool CreateTraining(CreateTrainingDTO createTrainingDTO)
        {

            return _itrainingDAL.CreateTraining(createTrainingDTO);
        }
        public bool UpdateTraining(TrainingModel trainingmodel)
        {
           return _itrainingDAL.UpdateTraining(trainingmodel);
        }
        public bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            return _itrainingDAL.AddPrerequisiteToTraining(trainingPrerequisiteModel);
        }
        public bool UpdateTrainingPrerequisite(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            return _itrainingDAL.UpdateTrainingPrerequisite(trainingPrerequisiteModel);
        }
        public bool DeleteTraining(byte TrainingId)
        {
            return _itrainingDAL.DeleteTraining(TrainingId);
        }
    }
}