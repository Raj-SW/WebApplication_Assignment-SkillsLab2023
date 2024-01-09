using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.DynamicData.ModelProviders;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace WebApplication_Assignment_SkillsLab2023.Services
{
    public class LocalFileHandlerService : IFileHandlerService
    {
        public HttpFileCollectionBase FileDownload(int userId, int trainingId)
        {
            throw new NotImplementedException();
        }

        public TaskResult FileUpload(int userId, int trainingId, HttpFileCollectionBase FileCollection)
        {
            TaskResult taskModelResult = new TaskResult();
            if (FileCollection.Count > 0)
            {
                    HttpFileCollectionBase files = FileCollection;
                    foreach (string fileName in files)
                    {
                        string uploadsFolder = System.Web.Hosting.HostingEnvironment.MapPath($"~/Storage/TrainingId-{trainingId}/UserId-{userId}");
                        Directory.CreateDirectory(uploadsFolder);
                        HttpPostedFileBase file = files[fileName];
                        string actualFileName = file.FileName;
                        string path = Path.Combine(uploadsFolder, actualFileName);
                        file.SaveAs(path);
                        taskModelResult.AddResultMessage(path);
                    }
                    taskModelResult.isSuccess= true;
                    return taskModelResult;
              
            }
            taskModelResult.isSuccess = false;
            taskModelResult.AddResultMessage("Internal Server Storage Error.\n No Files were received.\n We will get back to you");
            return taskModelResult;
        }
    }
}