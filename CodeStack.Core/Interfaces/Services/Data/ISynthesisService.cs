using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface ISynthesisService
{
    Task<Synthesis?> GetByIdAsync(Guid id);
    Task<IEnumerable<Synthesis>> GetByUserAsync(Guid userId, bool? isSnippet);
    Task<Synthesis?> CreateAsync(Guid userId, string title, string? description, string? content, bool isSnippet);
    Task<bool> UpdateAsync(Guid id, string? title, string? description, string? content);
    Task<bool> ArchiveAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}
