using CodeStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace CodeStack.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(User user);
        Task<User?> AddAsync(User user);
        Task UpdatePassword(Guid id, string password);
    }
}
