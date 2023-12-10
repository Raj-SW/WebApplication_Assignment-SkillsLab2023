using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class EnrolmentDTO
    {
        public TrainingModel trainingModel {get;set;}
        List<UserPrerequisiteModel> userPrerequisiteModels { get;set;}
    }
}