using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class GetPendingEmployeesEnrolmentOfAMangerDTO
    {
        public byte EnrolmentId {  get; set; }
        public byte TrainingId {  get; set; }
        public byte UserId {  get; set; }
        public string UserFirstName {  get; set; }
        public string UserLastName { get; set; }
        public string TrainingName {  get; set; }
        public DateTime EnrolmentDateTime { get; set; }
        public DateTime TrainingRegistrationDeadline { get; set; }
    }
}