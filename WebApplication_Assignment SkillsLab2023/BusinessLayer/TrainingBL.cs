using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<bool> AddPrerequisiteToTrainingAsync(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            return await _itrainingDAL.AddPrerequisiteToTrainingAsync(trainingPrerequisiteModel);
        }
        public async Task<bool> CreatePrerequisiteAsync(string description)
        {
            return await _itrainingDAL.CreatePrerequisiteAsync(description);
        }
        public async Task<bool> CreateTrainingAsync(CreateTrainingDTO createTrainingDTO)
        {
            return await _itrainingDAL.CreateTrainingAsync(createTrainingDTO);
        }
        #endregion

        #region Get Models
        public async Task<List<TrainingWithPrerequisitesModel>> GetAllTrainingModelsAsync()
        {
            return await _itrainingDAL.GetAllTrainingModelsAsync();
        }
        public async Task<List<TrainingWithPrerequisitesModel>> GetAllTrainingModelsWithPrerequisitesAsync()
        {

            var ListOftrainingModelsWithPrerequisites = await GetAllTrainingModelsAsync();
            foreach (var TrainingModelsWithPrerequisites in ListOftrainingModelsWithPrerequisites)
            {
                TrainingModelsWithPrerequisites.PrerequisitesList = await GetAllPrerequisiteOfATrainingModelByTrainingIdAsync(TrainingModelsWithPrerequisites.TrainingId);
            }

            return ListOftrainingModelsWithPrerequisites;
        }
        public async Task<List<PrerequisitesModel>> GetTrainingPrerequisitesByIdAsync(int trainingId)
        {
            return await _itrainingDAL.GetTrainingPrerequisitesByIdAsync(trainingId);
        }
        public async Task<List<PrerequisitesModel>> GetAllPrerequisiteOfATrainingModelByTrainingIdAsync(byte TrainingId)
        {
            return await _itrainingDAL.GetAllPrerequisiteOfATrainingModelByTrainingIdAsync(TrainingId);
        }
        public async Task<List<PrerequisitesModel>> GetAllPrerequisitesAsync()
        {
            return await _itrainingDAL.GetAllPrerequisitesAsync();
        }
        public async Task<string> GetTrainingNameByTrainingIdAsync(byte trainingId) {

            return await _itrainingDAL.GetTrainingNameByTrainingIdAsync(trainingId);
        }
        public Task<bool> DoesTrainingHavePrerequisitesAsync(byte trainingId)
        {
            return _itrainingDAL.DoesTrainingHavePrerequisitesAsync(trainingId);
        }
        #endregion

        #region Update Models
        public async Task<bool> UpdateTrainingAsync(TrainingModel trainingmodel)
        {
            return await _itrainingDAL.UpdateTrainingAsync(trainingmodel);
        }
        public async Task<bool> UpdateTrainingPrerequisiteAsync(byte TrainingId, List<byte> Prerequisites)
        {
            return await _itrainingDAL.UpdateTrainingPrerequisiteAsync(TrainingId, Prerequisites);
        }
        #endregion

        #region Delete Models
        public async Task<bool> DeleteTrainingAsync(byte TrainingId)
        {
            if (await isTrainingDeletableAsync(TrainingId))
            {
                return await _itrainingDAL.DeleteTrainingAsync(TrainingId);
            }
            return false;
        }
        public async Task<bool> isTrainingDeletableAsync(byte trainingId)
        {

            return await _itrainingDAL.isTrainingDeletableAsync(trainingId);
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