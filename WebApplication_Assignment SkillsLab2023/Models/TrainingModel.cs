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
        public byte TrainingId {  get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set;}
        public string TrainingStatus { get; set; }
        public byte DepartmentPriority {  get; set; }
        public byte SeatsAvailable {  get; set; }
        public byte SeatsTotal { get; set; }
        public byte CoachId { get; set; }
        public DateTime TrainingRegistrationDeadline { get; set; }
    }
}