using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    public class LoginVewModel
    {
        [Required]
        public string RawPassword { get; set; }
        [Required]
        public string Email { get; set; }

    }
}