
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface ITrainingBL
    {
        #region Insert Models
        bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel);
        bool CreatePrerequisite(string description);
        bool CreateTraining(CreateTrainingDTO createTrainingDTO);
        #endregion

        #region Get Models
        List<TrainingWithPrerequisitesModel> GetAllTrainingModels();
        List<TrainingWithPrerequisitesModel> GetAllTrainingModelsWithPrerequisites();
        List<PrerequisitesModel> GetTrainingPrerequisitesById(int trainingId);
        List<PrerequisitesModel> GetAllPrerequisiteOfATrainingModelByTrainingId(byte TrainingId);
        List<PrerequisitesModel> GetAllPrerequisites();
        string GetTrainingNameByTrainingId(byte trainingId);
        #endregion

        #region Update Models
        bool UpdateTraining(TrainingModel trainingmodel);
        bool UpdateTrainingPrerequisite(byte TrainingId, List<byte> Prerequisites);
        #endregion

        #region Delete Models
        bool DeleteTraining(byte id);
        bool isTrainingDeletable(byte trainingId);
        #endregion
       
    }
}
