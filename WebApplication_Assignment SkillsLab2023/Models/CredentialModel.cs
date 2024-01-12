using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Credential")]
public class CredentialModel
{
    [Key]
    public byte AccessId { get; set; }
    public byte UserId { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid Email Entered")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^[a-zA-Z0-9]{8,}$", ErrorMessage = "Invalid Password Entered")]
    public string RawPassword { get; set; }
    public byte[] Salt { get; set; }
    public byte[] Password { get; set; }
    public bool Activated { get; set; }
}
