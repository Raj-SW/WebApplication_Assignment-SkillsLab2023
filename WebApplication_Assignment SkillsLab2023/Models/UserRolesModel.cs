﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    public class UserRolesModel
    {
        public byte UserRoleId { get; set; }
        public byte UserId { get; set; }
        public byte RoleId { get; set; }
        public string RoleName {  get; set; }
    }
}