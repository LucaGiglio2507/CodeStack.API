using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Repositories;

public interface IEventRepository
{
    Task<Event?> GetByIdAsync(Guid id);
    Task<IEnumerable<Event>> GetByUserAsync(Guid userId);
    Task<Event?> CreateAsync(Event ev);
    Task<bool> UpdateAsync(Event ev);
    Task<bool> DeleteAsync(Guid id);
}
