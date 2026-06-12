using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface IEventService
{
    Task<Event?> GetByIdAsync(Guid id);
    Task<IEnumerable<Event>> GetByUserAsync(Guid userId);
    Task<Event?> CreateAsync(Guid userId, string title, string? description, DateTime startsAt);
    Task<bool> UpdateAsync(Guid id, string? title, string? description, DateTime? startsAt);
    Task<bool> DeleteAsync(Guid id);
}
