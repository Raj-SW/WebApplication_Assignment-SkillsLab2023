using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface ITrainingDAL
    {
        #region Get Model
        List<TrainingWithPrerequisitesModel> GetAllTrainingModels();
        List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId);
        List<PrerequisitesModel> GetAllPrerequisites();
        List<PrerequisitesModel> GetAllPrerequisiteOfATrainingModelByTrainingId(byte trainingId);
        #endregion

        #region Insert
        bool CreateTraining(CreateTrainingDTO createTraining);
        bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel);
        bool CreatePrerequisite(string description);
        #endregion

        #region Update
        bool UpdateTraining(TrainingModel trainingmodel);
        bool UpdateTrainingPrerequisite(byte TrainingId, List<byte> Prerequisites);
        #endregion

        #region Delete
        bool DeleteTraining(byte trainingId);
        bool isTrainingDeletable(byte trainingId);
        #endregion

    }
}
