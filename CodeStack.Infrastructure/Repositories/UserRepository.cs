using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for User: lookup by email or id, add, full update, and targeted password update.
/// </summary>
public class UserRepository(CodeStackDBContext _context) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task UpdatePassword(Guid id, string password)
    {
        User? user = await _context.Users.FindAsync(id);
        if (user is null) return;
        user.Password = password;
        await _context.SaveChangesAsync();
    }
}
