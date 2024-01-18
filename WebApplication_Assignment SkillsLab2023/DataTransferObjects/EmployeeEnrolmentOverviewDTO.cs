using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class EmployeeEnrolmentOverviewDTO
    {
        public byte UserId { get; set; }
        public byte TrainingId { get; set; }
        public byte EnrolmentId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string ManagerApproval {  get; set; }
        public string RegistrationStatus {  get; set; } 
        public DateTime EnrolmentDateTime { get; set; }
    }
}