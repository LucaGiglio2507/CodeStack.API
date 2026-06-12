using CodeStack.Domain.Entities;
using CodeStack.Domain.Enums;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface IKanbanService
{
    Task<Kanban?> GetByIdAsync(Guid id);
    Task<IEnumerable<Kanban>> GetByUserAsync(Guid userId);
    Task<Kanban?> CreateAsync(Guid creatorId, string name, string? description, string? iconUrl);
    Task<bool> UpdateAsync(Guid id, string? name, string? description, string? iconUrl);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddMemberAsync(Guid kanbanId, Guid userId, Roles role);
    Task<bool> RemoveMemberAsync(Guid kanbanId, Guid userId);
}
