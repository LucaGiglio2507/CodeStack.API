using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Kanban boards; eager-loads Creator, Members and Columns. Filters boards by creator or member. Handles the KanbanMember join-table (add/remove/check).
/// </summary>
public class KanbanRepository(CodeStackDBContext _context) : IKanbanRepository
{
    public async Task<Kanban?> GetByIdAsync(Guid id)
    {
        return await _context.Kanbans
            .Include(k => k.Creator)
            .Include(k => k.Members).ThenInclude(m => m.User)
            .Include(k => k.Columns)
            .FirstOrDefaultAsync(k => k.Id == id);
    }

    public async Task<IEnumerable<Kanban>> GetByUserAsync(Guid userId)
    {
        return await _context.Kanbans
            .Include(k => k.Creator)
            .Include(k => k.Members).ThenInclude(m => m.User)
            .Include(k => k.Columns)
            .Where(k => k.Creator_Id == userId || k.Members.Any(m => m.User_Id == userId))
            .ToListAsync();
    }

    public async Task<Kanban?> CreateAsync(Kanban kanban)
    {
        _context.Kanbans.Add(kanban);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(kanban.Id);
    }

    public async Task<bool> UpdateAsync(Kanban kanban)
    {
        _context.Kanbans.Update(kanban);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Kanban? kanban = await _context.Kanbans.FindAsync(id);
        if (kanban is null) return false;
        _context.Kanbans.Remove(kanban);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddMemberAsync(KanbanMember member)
    {
        _context.KanbanMembers.Add(member);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveMemberAsync(Guid kanbanId, Guid userId)
    {
        KanbanMember? member = await _context.KanbanMembers
            .FirstOrDefaultAsync(m => m.Kanban_Id == kanbanId && m.User_Id == userId);
        if (member is null) return false;
        _context.KanbanMembers.Remove(member);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> IsMemberAsync(Guid kanbanId, Guid userId)
    {
        return await _context.KanbanMembers
            .AnyAsync(m => m.Kanban_Id == kanbanId && m.User_Id == userId);
    }
}
