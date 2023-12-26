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
        List<TrainingModel> GetAllTrainingModels();
        List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId);
        bool EnrolEmployeeIntoTraining(int userId, int trainingId, List<string> filepath);
        List<PrerequisitesModel> GetAllPrerequisites();

        //int InsertIntoEnrolmentTable(int userId,int trainingId);
        //bool InsertIntoEnrolmentPrerequisiteTable(int enrolmentId, List<string> filepath);
    }
}
