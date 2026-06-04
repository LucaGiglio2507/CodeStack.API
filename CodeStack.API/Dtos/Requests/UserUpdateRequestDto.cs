using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests
{
    public class UserUpdateRequestDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Avatar URL must not exceed 500 characters.")]
        public string? AvatarUrl { get; set; }

        public bool CookieAccepted { get; set; }
    }
}
