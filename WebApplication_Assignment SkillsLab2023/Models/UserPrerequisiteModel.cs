using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("UserPrerequisite")]
    public class UserPrerequisiteModel
    {
        public byte PrerequisiteId { get; set; }
        public byte TrainingId { get; set; }
        public byte UserId { get; set; }
        public string PrerequisiteDescription { get; set; }
        public string FilePath { get; set; }
    }
}