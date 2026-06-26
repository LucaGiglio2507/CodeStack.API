using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Synthesis; ordered by Created_At descending. GetByUserAndType adds an IsSnippet filter.
/// </summary>
public class SynthesisRepository(CodeStackDBContext _context) : ISynthesisRepository
{
    public async Task<Synthesis?> GetByIdAsync(Guid id)
    {
        return await _context.Syntheses.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Synthesis>> GetByUserAsync(Guid userId)
    {
        return await _context.Syntheses
            .Where(s => s.User_Id == userId)
            .OrderByDescending(s => s.Created_At)
            .ToListAsync();
    }

    public async Task<IEnumerable<Synthesis>> GetByUserAndTypeAsync(Guid userId, bool isSnippet)
    {
        return await _context.Syntheses
            .Where(s => s.User_Id == userId && s.IsSnippet == isSnippet)
            .OrderByDescending(s => s.Created_At)
            .ToListAsync();
    }

    public async Task<IEnumerable<Synthesis>> GetByFolderAsync(Guid folderId)
    {
        return await _context.Syntheses
            .Where(s => s.FolderId == folderId)
            .OrderByDescending(s => s.Created_At)
            .ToListAsync();
    }

    public async Task<Synthesis?> CreateAsync(Synthesis synthesis)
    {
        _context.Syntheses.Add(synthesis);
        await _context.SaveChangesAsync();
        return synthesis;
    }

    public async Task<bool> UpdateAsync(Synthesis synthesis)
    {
        _context.Syntheses.Update(synthesis);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Synthesis? synthesis = await _context.Syntheses.FindAsync(id);
        if (synthesis is null) return false;
        _context.Syntheses.Remove(synthesis);
        return await _context.SaveChangesAsync() > 0;
    }
}
