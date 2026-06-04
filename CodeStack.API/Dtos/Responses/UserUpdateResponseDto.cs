using CodeStack.Domain.Entities;

namespace CodeStack.API.Dtos.Responses
{
    public class UserUpdateResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public bool CookieAccepted { get; set; }

        public static UserUpdateResponseDto FromUser(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.First_name,
            LastName = user.Name,
            AvatarUrl = user.Avatar_Url,
            CookieAccepted = user.CookieAccepted
        };
    }
}
