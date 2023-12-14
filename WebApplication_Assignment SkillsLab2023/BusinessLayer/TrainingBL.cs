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
            if (FileCollection.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = FileCollection;
                    // Create the uploads folder if it doesn't exist
                    string uploadsFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Storage/");
                    Directory.CreateDirectory(uploadsFolder);
                    // Loop through each file in the collection
                    foreach (string fileName in files)
                    {
                        HttpPostedFileBase file = files[fileName];
                        // Get the file name
                        string actualFileName = file.FileName;
                        // Combine the uploads folder path with the actual file name
                        string path = Path.Combine(uploadsFolder,actualFileName);
                        // Save the file
                        file.SaveAs(path);
                    }

                    return true;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return false;
        }
    }
}