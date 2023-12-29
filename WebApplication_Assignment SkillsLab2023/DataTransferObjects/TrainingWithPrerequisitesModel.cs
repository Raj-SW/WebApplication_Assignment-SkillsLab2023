using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class TrainingWithPrerequisitesModel
    {
        public byte TrainingId { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingStatus { get; set; }
        public string DepartmentPriority { get; set; }
        public byte SeatsAvailable { get; set; }
        public byte SeatsTotal { get; set; }
        public byte CoachId { get; set; }
        public DateTime TrainingRegistrationDeadline { get; set; }
        public List<PrerequisitesModel> PrerequisitesList { get; set; }
    }
}