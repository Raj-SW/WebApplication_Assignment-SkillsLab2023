using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class EnrolmentDTO
    {
        public byte TrainingId {get;set;}
        public byte UserId {get;set; }
        public List<HttpPostedFileBase> Files { get;set;}
    }
}