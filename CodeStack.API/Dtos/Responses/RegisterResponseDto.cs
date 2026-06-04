using CodeStack.Domain.Entities;

namespace CodeStack.API.Dtos.Responses
{
    public class RegisterResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public static RegisterResponseDto FromUser(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.First_name,
            LastName = user.Name,
            CreatedAt = user.Created_At,
            IsActive = user.IsActive
        };
    }
}
