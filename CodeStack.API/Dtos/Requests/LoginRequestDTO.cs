using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests
{
    public class LoginRequestDTO
    {
        public class LoginRequestDto
        {
            [Required(ErrorMessage = "The email is required.")]
            public required string Email { get; set; } = null!;

            [Required(ErrorMessage = "The password is required.")]
            public required string HashedPassword { get; set; } = null!;
        }
    }
}
