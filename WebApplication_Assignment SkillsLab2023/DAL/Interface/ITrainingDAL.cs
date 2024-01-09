using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface ITrainingDAL
    {
        #region Get Model
        Task<List<TrainingWithPrerequisitesModel>> GetAllTrainingModelsAsync();
        Task<List<PrerequisitesModel>> GetTrainingPrerequisitesByIdAsync(int trainingId);
        Task<List<PrerequisitesModel>> GetAllPrerequisitesAsync();
        Task<List<PrerequisitesModel>> GetAllPrerequisiteOfATrainingModelByTrainingIdAsync(byte trainingId);
        Task<string> GetTrainingNameByTrainingIdAsync(byte trainingId);
        #endregion

        #region Insert
        Task<bool> CreateTrainingAsync(CreateTrainingDTO createTraining);
        Task<bool> AddPrerequisiteToTrainingAsync(TrainingPrerequisiteModel trainingPrerequisiteModel);
        Task<bool> CreatePrerequisiteAsync(string description);
        #endregion

        #region Update
        Task<bool> UpdateTrainingAsync(TrainingModel trainingmodel);
        Task<bool> UpdateTrainingPrerequisiteAsync(byte TrainingId, List<byte> Prerequisites);
        #endregion

        #region Delete
        Task<bool> DeleteTrainingAsync(byte trainingId);
        Task<bool> isTrainingDeletableAsync(byte trainingId);
        #endregion
    }
}
