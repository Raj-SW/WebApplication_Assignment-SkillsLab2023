using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;
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
        public List<TrainingModel> GetAllTraining()
        {
            
            return _itrainingDAL.GetAllTrainingModels();
        }
        public List<TrainingPrerequisiteModel> GetTrainingPrerequisitesById(int trainingId)
        {
            return _itrainingDAL.GetTrainingPrerequisitesById(trainingId);
        }

        public bool EnrolEmployeeIntoTraining(int userId, int trainingId, HttpFileCollectionBase FileCollection)
        {
            TaskResult uploadTaskResult= new TaskResult();
            //TODO:
            //Security protocols here
            //Upload files
            uploadTaskResult = _iFileHandlerService.FileUpload(userId,trainingId,FileCollection);
            //Get paths of User submittedPrerequisites
            //Insert into Enrolment table
            if (uploadTaskResult.isSuccess)
            {
                //Get the EnrolmentID
                var enrolmentId = InsertEnrolmentDetailsIntoDatabase(userId,trainingId,uploadTaskResult);
                //Insert n number of User Uploaded files into EnrolmentPrerequisite
                foreach(String FilePath in uploadTaskResult.ResultMessageList)
                {
                    var isFileInserted = InsertAttachmentDetailsIntoEnrolmentPrerequisiteTable(enrolmentId, FilePath);
                }
            }
            return true;
        }
        public int InsertEnrolmentDetailsIntoDatabase(int userId, int trainingId, TaskResult uploadTaskResult)
        {
            //Insert into the Enrolment Table
            var enrolmentId=_itrainingDAL.InsertIntoEnrolmentTable(userId,trainingId);
            //and EnrolmentPrerequisiteTable
            return enrolmentId;
        }
        public bool InsertAttachmentDetailsIntoEnrolmentPrerequisiteTable(int enrolmentId, string filepath)
        {
            return _itrainingDAL.InsertIntoEnrolmentPrerequisiteTable( enrolmentId,filepath);
        }
    }
}