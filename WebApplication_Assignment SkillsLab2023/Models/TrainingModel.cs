using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("Training")]
    public class TrainingModel
    {
        public int TrainingId {  get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set;}
        public string TrainingStatus { get; set; }
        public string DepartmentPriority {  get; set; }
        public int SeatsAvailable {  get; set; }
        public int SeatsTotal { get; set; }
        public DateTime TrainingRegistrationDeadline { get; set; }
    }
}