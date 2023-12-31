using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("TrainingRegistration")]
    public class TrainingRegistrationModel
    {
        public byte RegistrationId { get; set; }
        public byte TrainingID { get; set;}
        public byte UserId { get; set; }
        public string RegistrationStatus { get; set; }
        public bool ManagerApproval { get; set; }

    }
}