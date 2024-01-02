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
        public List<TrainingWithPrerequisitesModel> GetAllTrainingModelsWithPrerequisites()
        {
            
            var ListOftrainingModelsWithPrerequisites= GetAllTrainingModels();
            foreach ( var TrainingModelsWithPrerequisites in ListOftrainingModelsWithPrerequisites)
            {
                TrainingModelsWithPrerequisites.PrerequisitesList=GetAllPrerequisiteOfATrainingModelByTrainingId(TrainingModelsWithPrerequisites.TrainingId);
            }

            return ListOftrainingModelsWithPrerequisites;
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
        public bool UpdateTrainingPrerequisite(byte TrainingId, List<byte> Prerequisites)
        {
            return _itrainingDAL.UpdateTrainingPrerequisite(TrainingId, Prerequisites);
        }
        public bool DeleteTraining(byte TrainingId)
        {
            if (isTrainingDeletable(TrainingId))
            {
                return _itrainingDAL.DeleteTraining(TrainingId);
            }
            return false;
        }
        public List<PrerequisitesModel> GetAllPrerequisiteOfATrainingModelByTrainingId(byte TrainingId)
        {
            return _itrainingDAL.GetAllPrerequisiteOfATrainingModelByTrainingId( TrainingId);
        }
        public List<TrainingWithPrerequisitesModel> GetAllTrainingModels()
        {
            return _itrainingDAL.GetAllTrainingModels();
        }
        public bool isTrainingDeletable(byte trainingId)
        {

            return _itrainingDAL.isTrainingDeletable(trainingId);
        }
        public List<UserPrerequisiteModel> GetEnrolmentPrerequisitesOfAUserByEnrolmentId(byte enrolmentId)
        {

            return _itrainingDAL.GetEnrolmentPrerequisitesOfAUserByEnrolmentId(enrolmentId);
        }

    }
}