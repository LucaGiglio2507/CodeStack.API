using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface ITagService
{
    Task<Tag?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tag>> GetByUserAsync(Guid userId);
    Task<Tag?> CreateAsync(Guid userId, string name, string color);
    Task<bool> UpdateAsync(Guid id, string? name, string? color);
    Task<bool> DeleteAsync(Guid id);
}
