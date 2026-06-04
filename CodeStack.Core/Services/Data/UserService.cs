using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Core.Interfaces.Services.Tools;
using CodeStack.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace CodeStack.Core.Services.Data;

public class UserService(IUserRepository _userRepository, IPasswordHasherService _passwordHasher) : IUserService
{
    public async Task<User?> GetByIdAsync(Guid id)
        => await _userRepository.GetByIdAsync(id);

    public async Task UpdatePassword(Guid id, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return;

        if (!_passwordHasher.VerifyPassword(currentPassword, user.Password)) return;

        var hashed = _passwordHasher.HashPassword(newPassword);
        await _userRepository.UpdatePassword(id, hashed);
    }

    public async Task<User?> UpdateUserAsync(Guid id, string firstName, string lastName, string? avatarUrl, bool cookieAccepted)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return null;

        user.First_name = firstName;
        user.Name = lastName;
        user.Avatar_Url = avatarUrl;
        user.CookieAccepted = cookieAccepted;

        await _userRepository.UpdateAsync(user);
        return user;
    }
}

