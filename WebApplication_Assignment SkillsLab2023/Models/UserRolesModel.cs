using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    public class UserRolesModel
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName {  get; set; }
    }
}