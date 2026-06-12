using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Repositories;

public interface IFolderRepository
{
    Task<Folder?> GetByIdAsync(Guid id);
    Task<IEnumerable<Folder>> GetRootFoldersByUserAsync(Guid userId);
    Task<IEnumerable<Folder>> GetSubFoldersAsync(Guid parentId);
    Task<Folder?> CreateAsync(Folder folder);
    Task<bool> UpdateAsync(Folder folder);
    Task<bool> DeleteAsync(Guid id);
}
