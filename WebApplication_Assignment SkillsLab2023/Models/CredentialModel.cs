﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("Credential")]
    public class CredentialModel
    {
        [Key]
        public int UserId { get; set; }
        public int AccessId { get; set; }
        public string Password { get; set; }

    }
}