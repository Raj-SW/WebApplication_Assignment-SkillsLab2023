using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace WebApplication_Assignment_SkillsLab2023.Services
{
    public class LocalFileHandlerService : IFileHandlerService
    {
        public HttpFileCollectionBase FileDownload(int userId, int trainingId)
        {
            throw new NotImplementedException();
        }

        public bool FileUpload(int userId, int trainingId, HttpFileCollectionBase FileCollection)
        {
            if (FileCollection.Count > 0)
            {
                try
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