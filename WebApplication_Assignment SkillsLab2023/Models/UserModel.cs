using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication_Assignment_SkillsLab2023.Models.Others;

namespace WebApplication_Assignment_SkillsLab2023.Models
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        public byte UserId { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string NIC { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("/^[a-zA-Z]{2,}$/",ErrorMessage = "Invalid First Name Entered")]
        public string UserFirstName { get; set; }
        [RegularExpression("/^[a-zA-Z]{2,}$/",ErrorMessage = "Invalid Last Name Entered")]
        [Required(ErrorMessage = "Field is required")]
        public string UserLastName { get; set; }
        public byte DepartmentId {  get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(" /^\\d{8}$/", ErrorMessage = "Invalid Mobile Number Entered")]
        public string MobileNum {  get; set; }
        public string Role { get; set; }
    }
}