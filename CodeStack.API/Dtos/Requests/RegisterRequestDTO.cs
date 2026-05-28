using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(250, ErrorMessage = "Maximum length must be 250 characters.")]
        [RegularExpression(@"^.{2,}@.{2,}\..+$", ErrorMessage = "Invalid format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } = null!;
    }
}
