using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class ActivationDTO
    {
            public byte UserId { get; set; }
            public byte DepartmentId { get; set; }
            public byte ManagerId { get; set; }
            public byte RoleId { get; set; }
    }
}