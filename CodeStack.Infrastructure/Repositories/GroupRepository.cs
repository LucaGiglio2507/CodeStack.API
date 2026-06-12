using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Group; includes Participants. GetByUser filters on participant collection. AddParticipant loads the navigation property and appends the User entity.
/// </summary>
public class GroupRepository(CodeStackDBContext _context) : IGroupRepository
{
    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await _context.Groups
            .Include(g => g.Participants)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Group>> GetByUserAsync(Guid userId)
    {
        return await _context.Groups
            .Include(g => g.Participants)
            .Where(g => g.Participants.Any(p => p.Id == userId))
            .ToListAsync();
    }

    public async Task<Group?> CreateAsync(Group group)
    {
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();
        return group;
    }

    public async Task<bool> AddParticipantAsync(Guid groupId, Guid userId)
    {
        Group? group = await _context.Groups
            .Include(g => g.Participants)
            .FirstOrDefaultAsync(g => g.Id == groupId);
        User? user = await _context.Users.FindAsync(userId);
        if (group is null || user is null) return false;
        group.Participants.Add(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> IsParticipantAsync(Guid groupId, Guid userId)
    {
        return await _context.Groups
            .AnyAsync(g => g.Id == groupId && g.Participants.Any(p => p.Id == userId));
    }
}
