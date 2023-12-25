using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("Department")]
    public class DepartmentModel
    {
        public byte DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public byte NoOfEmployees {  get; set; }
    }
}