using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication_Assignment_SkillsLab2023.Models.Others;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string NIC { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int DepartmentId {  get; set; }
        public string MobileNum {  get; set; }
        public string Role { get; set; }
    }
}