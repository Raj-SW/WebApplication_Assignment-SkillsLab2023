using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class CreateTrainingDTO
    {
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingStatus { get; set; }
        public byte DepartmentPriority { get; set; } 
        public byte TotalSeats { get; set; } 
        public DateTime RegistrationDeadline { get; set; } 
        public DateTime DateCreated { get; set; }
        public byte Coach { get; set; } 
        public List<byte> Prerequisites { get; set; }

    }
}