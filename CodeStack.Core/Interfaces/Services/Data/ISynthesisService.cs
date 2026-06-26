using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface ISynthesisService
{
    Task<Synthesis?> GetByIdAsync(Guid id);
    Task<IEnumerable<Synthesis>> GetByUserAsync(Guid userId, bool? isSnippet);
    Task<IEnumerable<Synthesis>> GetByFolderAsync(Guid folderId);
    Task<Synthesis?> CreateAsync(Guid userId, string title, string? description, string? content, bool isSnippet, Guid? folderId);
    Task<bool> UpdateAsync(Guid id, string? title, string? description, string? content, Guid? folderId);
    Task<bool> ArchiveAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
