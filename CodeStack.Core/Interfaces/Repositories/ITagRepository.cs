using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Repositories;

public interface ITagRepository
{
    Task<Tag?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tag>> GetByUserAsync(Guid userId);
    Task<Tag?> CreateAsync(Tag tag);
    Task<bool> UpdateAsync(Tag tag);
    Task<bool> DeleteAsync(Guid id);
}
