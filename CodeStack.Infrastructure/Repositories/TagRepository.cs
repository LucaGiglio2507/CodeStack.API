using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Tag; eager-loads User on single-item fetch, filters by User_Id on list.
/// </summary>
public class TagRepository(CodeStackDBContext _context) : ITagRepository
{
    public async Task<Tag?> GetByIdAsync(Guid id)
    {
        return await _context.Tags
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tag>> GetByUserAsync(Guid userId)
    {
        return await _context.Tags
            .Where(t => t.User_Id == userId)
            .ToListAsync();
    }

    public async Task<Tag?> CreateAsync(Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<bool> UpdateAsync(Tag tag)
    {
        _context.Tags.Update(tag);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Tag? tag = await _context.Tags.FindAsync(id);
        if (tag is null) return false;
        _context.Tags.Remove(tag);
        return await _context.SaveChangesAsync() > 0;
    }
}
