using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace WebApplication_Assignment_SkillsLab2023.Services
{
    public class LocalFileHandlerService : IFileHandlerService
    {
        public bool FileUpload()
        {
            return true;
        }
    }
}