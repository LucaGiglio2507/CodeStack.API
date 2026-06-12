using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Event; returns a user's events ordered by Starts_At ascending.
/// </summary>
public class EventRepository(CodeStackDBContext _context) : IEventRepository
{
    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Event>> GetByUserAsync(Guid userId)
    {
        return await _context.Events
            .Where(e => e.User_Id == userId)
            .OrderBy(e => e.Starts_At)
            .ToListAsync();
    }

    public async Task<Event?> CreateAsync(Event ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
        return ev;
    }

    public async Task<bool> UpdateAsync(Event ev)
    {
        _context.Events.Update(ev);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Event? ev = await _context.Events.FindAsync(id);
        if (ev is null) return false;
        _context.Events.Remove(ev);
        return await _context.SaveChangesAsync() > 0;
    }
}
