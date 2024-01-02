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
        List<TrainingWithPrerequisitesModel> GetAllTrainingModels();
        List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId);
        bool EnrolEmployeeIntoTraining(int userId, int trainingId, List<string> filepath);
        List<PrerequisitesModel> GetAllPrerequisites();
        bool CreateTraining(CreateTrainingDTO createTraining);
        bool UpdateTraining(TrainingModel trainingmodel);
        bool AddPrerequisiteToTraining(TrainingPrerequisiteModel trainingPrerequisiteModel);
        bool UpdateTrainingPrerequisite(byte TrainingId, List<byte> Prerequisites);
        bool DeleteTraining(byte trainingId);
        bool isTrainingDeletable(byte trainingId);
        List<PrerequisitesModel> GetAllPrerequisiteOfATrainingModelByTrainingId(byte trainingId);
        List<UserPrerequisiteModel> GetEnrolmentPrerequisitesOfAUserByEnrolmentId(byte enrolmentId);
        bool ApproveEnrolment(byte enrolmentId);
        bool RejectEnrolment(byte enrolmentId,string remarks);
        bool CreatePrerequisite(string description);
    }
}
