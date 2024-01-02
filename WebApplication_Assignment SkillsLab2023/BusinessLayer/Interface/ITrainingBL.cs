
using System.Collections.Generic;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface ITrainingBL
    {
        List<TrainingWithPrerequisitesModel> GetAllTrainingModels();
        List<TrainingWithPrerequisitesModel> GetAllTrainingModelsWithPrerequisites();
        List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId);
        bool EnrolEmployeeIntoTraining(int userId, int trainingId, HttpFileCollectionBase files);
        List<PrerequisitesModel> GetAllPrerequisiteOfATrainingModelByTrainingId(byte TrainingId);
        List<PrerequisitesModel> GetAllPrerequisites();
        bool CreateTraining(CreateTrainingDTO createTrainingDTO);
        bool UpdateTraining(TrainingModel trainingmodel);
        bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel);
        bool UpdateTrainingPrerequisite(byte TrainingId, List<byte> Prerequisites);
        bool DeleteTraining(byte id);
        bool isTrainingDeletable(byte trainingId);
        List<UserPrerequisiteModel> GetEnrolmentPrerequisitesOfAUserByEnrolmentId(byte enrolmentId);

    }
}
