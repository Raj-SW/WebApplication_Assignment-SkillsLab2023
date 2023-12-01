using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("Registration")]
    public class RegistrationModel
    {
        public int RegistrationId { get; set; }
        public int TrainingID { get; set;}
        public int UserId { get; set; }
        public string RegistrationStatus { get; set; }
        public bool ManagerApproval { get; set; }

    }
}