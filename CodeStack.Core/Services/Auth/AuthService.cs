using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Auth;
using CodeStack.Core.Interfaces.Services.Tools;
using CodeStack.Domain.Entities;
using CodeStack.Domain.Enums;

namespace CodeStack.Core.Services.Auth;

/// <summary>
/// Handles user authentication: validates credentials and account status on login, enforces email uniqueness on registration.
/// </summary>
public class AuthService(IUserRepository _userRepository, IPasswordHasherService _passwordHasher) : IAuthService
{
    public async Task<User> LoginAsync(string email, string password)
    {
        User? user = await _userRepository.GetByEmailAsync(email);
        if (user is null || !_passwordHasher.VerifyPassword(password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Account is disabled.");

        return user;
    }

    public async Task<User> RegisterAsync(string email, string firstName, string lastName,string password)
    {

        User? existing = await _userRepository.GetByEmailAsync(email);
        if (existing is not null)
            throw new ArgumentException("An account with this email already exists.");

        User user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            First_name = firstName,
            Name = lastName,
            Password = _passwordHasher.HashPassword(password),
            Role = Domain.Enums.Roles.User,
            Created_At = DateTime.UtcNow,
            IsActive = true,
            IsActivated = false
        };

        return await _userRepository.AddAsync(user)
            ?? throw new Exception("Failed to create user.");
    }
}
