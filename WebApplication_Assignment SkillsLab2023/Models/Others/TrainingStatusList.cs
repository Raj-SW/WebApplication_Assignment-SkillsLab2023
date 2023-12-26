using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models.Others
{
    public class TrainingStatusList
    {
        public List<String> ListOfTrainingStatus {  get; set; }
        public TrainingStatusList() 
        {
            ListOfTrainingStatus = new List<String>() {"Open", "Closed", "On Review", "Ongoing"};
        }
    }
}