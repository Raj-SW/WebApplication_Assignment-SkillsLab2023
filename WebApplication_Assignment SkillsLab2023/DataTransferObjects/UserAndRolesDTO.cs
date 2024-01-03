using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.DataTransferObjects
{
    public class UserAndRolesDTO
    {
        public byte UserId { get; set; }
        public string NIC { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public byte DepartmentId { get; set; }
        public byte ManagerId { get; set; }
        public string MobileNum { get; set; }
        public List<byte> Roles { get; set; }
    }
}