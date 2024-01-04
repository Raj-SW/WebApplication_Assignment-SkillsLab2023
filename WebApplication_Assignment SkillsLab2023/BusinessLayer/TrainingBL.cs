using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services;
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

        #region Insert Models
        public bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            return _itrainingDAL.AddPrerequisiteToTraining(trainingPrerequisiteModel);
        }
        public bool CreatePrerequisite(string description)
        {
            return _itrainingDAL.CreatePrerequisite(description);
        }
        public bool CreateTraining(CreateTrainingDTO createTrainingDTO)
        {

            return _itrainingDAL.CreateTraining(createTrainingDTO);
        }
        #endregion

        #region Get Models
        public List<TrainingWithPrerequisitesModel> GetAllTrainingModels()
        {
            return _itrainingDAL.GetAllTrainingModels();
        }
        public List<TrainingWithPrerequisitesModel> GetAllTrainingModelsWithPrerequisites()
        {

            var ListOftrainingModelsWithPrerequisites = GetAllTrainingModels();
            foreach (var TrainingModelsWithPrerequisites in ListOftrainingModelsWithPrerequisites)
            {
                TrainingModelsWithPrerequisites.PrerequisitesList = GetAllPrerequisiteOfATrainingModelByTrainingId(TrainingModelsWithPrerequisites.TrainingId);
            }

            return ListOftrainingModelsWithPrerequisites;
        }
        public List<PrerequisitesModel> GetTrainingPrerequisitesById(int trainingId)
        {
            return _itrainingDAL.GetTrainingPrerequisitesById(trainingId);
        }
        public List<PrerequisitesModel> GetAllPrerequisiteOfATrainingModelByTrainingId(byte TrainingId)
        {
            return _itrainingDAL.GetAllPrerequisiteOfATrainingModelByTrainingId(TrainingId);
        }
        public List<PrerequisitesModel> GetAllPrerequisites()
        {
            return _itrainingDAL.GetAllPrerequisites();
        }
        #endregion

        #region Update Models
        public bool UpdateTraining(TrainingModel trainingmodel)
        {
            return _itrainingDAL.UpdateTraining(trainingmodel);
        }
        public bool UpdateTrainingPrerequisite(byte TrainingId, List<byte> Prerequisites)
        {
            return _itrainingDAL.UpdateTrainingPrerequisite(TrainingId, Prerequisites);
        }
        #endregion

        #region Delete Models
        public bool DeleteTraining(byte TrainingId)
        {
            if (isTrainingDeletable(TrainingId))
            {
                return _itrainingDAL.DeleteTraining(TrainingId);
            }
            return false;
        }
        public bool isTrainingDeletable(byte trainingId)
        {

            return _itrainingDAL.isTrainingDeletable(trainingId);
        }
        #endregion

        #region Notification
        public string NotifyUser() {

            return "";
        }
        public string NotifyManager()
        {

            return "";
        }
        #endregion
    }
}