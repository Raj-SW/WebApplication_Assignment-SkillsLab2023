using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("TrainingPrerequisite")]
    public class TrainingPrerequisiteModel
    {
        public byte PrerequisiteId { get; set; }
        public byte TrainingId { get; set; }
        public string PrerequisiteDescription { get; set; }
    }
}