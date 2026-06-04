using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string email, string firstName, string lastName, string password);
    }
}
