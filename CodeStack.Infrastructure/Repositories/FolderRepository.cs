using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Folder; eager-loads SubFolders and Tags. Root-folder query filters on Parent_Folder_Id == null.
/// </summary>
public class FolderRepository(CodeStackDBContext _context) : IFolderRepository
{
    public async Task<Folder?> GetByIdAsync(Guid id)
    {
        return await _context.Folders
            .Include(f => f.SubFolders)
            .Include(f => f.Tags)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Folder>> GetRootFoldersByUserAsync(Guid userId)
    {
        return await _context.Folders
            .Include(f => f.SubFolders)
            .Include(f => f.Tags)
            .Where(f => f.User_Id == userId && f.Parent_Folder_Id == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<Folder>> GetSubFoldersAsync(Guid parentId)
    {
        return await _context.Folders
            .Include(f => f.SubFolders)
            .Include(f => f.Tags)
            .Where(f => f.Parent_Folder_Id == parentId)
            .ToListAsync();
    }

    public async Task<Folder?> CreateAsync(Folder folder)
    {
        _context.Folders.Add(folder);
        await _context.SaveChangesAsync();
        return folder;
    }

    public async Task<bool> UpdateAsync(Folder folder)
    {
        _context.Folders.Update(folder);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Folder? folder = await _context.Folders.FindAsync(id);
        if (folder is null) return false;
        _context.Folders.Remove(folder);
        return await _context.SaveChangesAsync() > 0;
    }
}
