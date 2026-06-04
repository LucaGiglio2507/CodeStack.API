using CodeStack.Domain.Entities;

namespace CodeStack.API.Dtos.Responses
{
    public static class UserDisableResponseExtensions
    {
        public static UserDisableResponseDto ToDisableResponseDto(this User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            IsActivated = user.IsActivated,
            UpdatedAt = user.Last_Login ?? DateTime.UtcNow
        };
    }
}
