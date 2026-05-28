using CodeStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace CodeStack.Core.Interfaces.Services.Data
{
    public interface IUserService
    {
        Task UpdatePassword(Guid id, string currentPassword, string newPassword);
        Task<User?> GetByIdAsync(Guid id);
    }
}
