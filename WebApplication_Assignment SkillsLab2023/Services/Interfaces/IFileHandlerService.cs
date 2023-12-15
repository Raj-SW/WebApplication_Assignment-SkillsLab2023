using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.Services.Interfaces
{
    public interface IFileHandlerService
    {
        TaskResult FileUpload(int userId, int trainingId, HttpFileCollectionBase FileCollection);
        HttpFileCollectionBase FileDownload(int userId, int trainingId);
    }
}
