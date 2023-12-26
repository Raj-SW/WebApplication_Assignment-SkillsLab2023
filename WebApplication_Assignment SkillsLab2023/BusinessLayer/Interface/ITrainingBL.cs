
using System.Collections.Generic;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface ITrainingBL
    {
        List<TrainingModel> GetAllTraining();
        List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId);
        bool EnrolEmployeeIntoTraining(int userId, int trainingId, HttpFileCollectionBase files);
        List<PrerequisitesModel> GetAllPrerequisites();
    }
}
