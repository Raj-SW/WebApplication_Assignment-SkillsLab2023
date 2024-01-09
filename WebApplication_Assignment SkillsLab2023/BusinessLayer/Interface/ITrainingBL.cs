using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface ITrainingBL
    {
        #region Insert Models
        Task<bool> AddPrerequisiteToTrainingAsync(TrainingPrerequisiteModel trainingPrerequisiteModel);
        Task<bool> CreatePrerequisiteAsync(string description);
        Task<bool> CreateTrainingAsync(CreateTrainingDTO createTrainingDTO);
        #endregion

        #region Get Models
        Task<List<TrainingWithPrerequisitesModel>> GetAllTrainingModelsAsync();
        Task<List<TrainingWithPrerequisitesModel>> GetAllTrainingModelsWithPrerequisitesAsync();
        Task<List<PrerequisitesModel>> GetTrainingPrerequisitesByIdAsync(int trainingId);
        Task<List<PrerequisitesModel>> GetAllPrerequisiteOfATrainingModelByTrainingIdAsync(byte TrainingId);
        Task<List<PrerequisitesModel>> GetAllPrerequisitesAsync();
        Task<string> GetTrainingNameByTrainingIdAsync(byte trainingId);
        #endregion

        #region Update Models
        Task<bool> UpdateTrainingAsync(TrainingModel trainingmodel);
        Task<bool> UpdateTrainingPrerequisiteAsync(byte TrainingId, List<byte> Prerequisites);
        #endregion

        #region Delete Models
        Task<bool> DeleteTrainingAsync(byte id);
        Task<bool> isTrainingDeletableAsync(byte trainingId);
        #endregion
    }
}
