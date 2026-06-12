using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// CRUD for hierarchical folders with optional archiving; distinguishes root folders (no parent) from sub-folders.
/// </summary>
public class FolderService(IFolderRepository _folderRepository) : IFolderService
{
    public async Task<Folder?> GetByIdAsync(Guid id)
    {
        return await _folderRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Folder>> GetRootFoldersByUserAsync(Guid userId)
    {
        return await _folderRepository.GetRootFoldersByUserAsync(userId);
    }

    public async Task<IEnumerable<Folder>> GetSubFoldersAsync(Guid parentId)
    {
        return await _folderRepository.GetSubFoldersAsync(parentId);
    }

    public async Task<Folder?> CreateAsync(Guid userId, string title, string? description, string? icon, string? color, Guid? parentFolderId)
    {
        Folder folder = new Folder
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Icon = icon,
            Color = color,
            Archived = false,
            Created_At = DateTime.UtcNow,
            User_Id = userId,
            Parent_Folder_Id = parentFolderId
        };
        return await _folderRepository.CreateAsync(folder);
    }

    public async Task<bool> UpdateAsync(Guid id, string? title, string? description, string? icon, string? color, bool? archived)
    {
        Folder? folder = await _folderRepository.GetByIdAsync(id);
        if (folder is null) return false;

        if (title is not null) folder.Title = title;
        if (description is not null) folder.Description = description;
        if (icon is not null) folder.Icon = icon;
        if (color is not null) folder.Color = color;
        if (archived is not null) folder.Archived = archived.Value;

        return await _folderRepository.UpdateAsync(folder);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _folderRepository.DeleteAsync(id);
    }
}
