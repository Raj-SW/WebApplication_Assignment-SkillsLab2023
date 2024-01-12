using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.CustomeServerSideValidations;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    [CustomServerSideValidation]
    public class LoginDTO
    {
        public string Email { get; set; }
        public string RawPassword { get; set; }
    }
}