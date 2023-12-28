
using System.Collections.Generic;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface ITrainingBL
    {
        List<TrainingModel> GetAllTraining();
        List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId);
        bool EnrolEmployeeIntoTraining(int userId, int trainingId, HttpFileCollectionBase files);
        List<PrerequisitesModel> GetAllPrerequisites();
        bool CreateTraining(CreateTrainingDTO createTrainingDTO);
        bool UpdateTraining(TrainingModel trainingmodel);
        bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel);
        bool UpdateTrainingPrerequisite(TrainingPrerequisiteModel trainingPrerequisiteModel);
        bool DeleteTraining(byte id);
    }
}
