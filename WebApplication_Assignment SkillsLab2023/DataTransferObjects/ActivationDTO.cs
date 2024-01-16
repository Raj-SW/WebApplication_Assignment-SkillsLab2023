using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class ActivationDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public byte UserId { get; set; }
        [Required(ErrorMessage = "User must be assigned to a department")]
        public byte DepartmentId { get; set; }
        public byte ManagerId { get; set; }
        [Required(ErrorMessage = "User Role is required")]
        public byte RoleId { get; set; }
    }
}