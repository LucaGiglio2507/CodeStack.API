using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Repositories;

public interface ISynthesisRepository
{
    Task<Synthesis?> GetByIdAsync(Guid id);
    Task<IEnumerable<Synthesis>> GetByUserAsync(Guid userId);
    Task<IEnumerable<Synthesis>> GetByUserAndTypeAsync(Guid userId, bool isSnippet);
    Task<IEnumerable<Synthesis>> GetByFolderAsync(Guid folderId);
    Task<Synthesis?> CreateAsync(Synthesis synthesis);
    Task<bool> UpdateAsync(Synthesis synthesis);
    Task<bool> DeleteAsync(Guid id);
}
