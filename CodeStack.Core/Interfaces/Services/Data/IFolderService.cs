using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface IFolderService
{
    Task<Folder?> GetByIdAsync(Guid id);
    Task<IEnumerable<Folder>> GetRootFoldersByUserAsync(Guid userId);
    Task<IEnumerable<Folder>> GetSubFoldersAsync(Guid parentId);
    Task<Folder?> CreateAsync(Guid userId, string title, string? description, string? icon, string? color, Guid? parentFolderId);
    Task<bool> UpdateAsync(Guid id, string? title, string? description, string? icon, string? color, bool? archived);
    Task<bool> DeleteAsync(Guid id);
}
