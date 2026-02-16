using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(150)]
        [RegularExpression(@"^[\p{L}]+(?:\s[\p{L}]+)*$",
            ErrorMessage = "Full name must contain letters only")]
        [DefaultValue("Hassan Ali")]
        public string FullName { get; set; } 

        [Required]
        [EmailAddress]
        [MaxLength(30)]
        [DefaultValue("hassan@example.com")]
        public string Email { get; set; } 

        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^[\p{L}\s]+$")]
        [DefaultValue("CS")]
        public string Department { get; set; } 

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Password must contain Numbers only")]
        [DefaultValue("0551234567")]
        public string PhoneNumber { get; set; }


        [Required]
        [DefaultValue("User")]

        public string Role { get; set; }

    }
}
