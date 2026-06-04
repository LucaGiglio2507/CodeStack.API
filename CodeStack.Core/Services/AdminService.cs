using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services;
using CodeStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeStack.Core.Services
{
    public class AdminService(IUserRepository _userRepository) : IAdminService
    {
        public async Task<User?> DisableUserAsync(Guid userId, Guid adminId)
        {
            if (userId == adminId) return null;

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !user.IsActivated) return null;

            user.IsActivated = false;
            user.Last_Login = DateTime.UtcNow;

            var success = await _userRepository.UpdateAsync(user);

            return success ? user : null;
        }
    }
}
