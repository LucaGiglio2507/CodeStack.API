using CodeStack.Domain.Entities;
using CodeStack.Domain.Enums;

namespace CodeStack.Core.Interfaces.Repositories;

public interface IKanbanRepository
{
    Task<Kanban?> GetByIdAsync(Guid id);
    Task<IEnumerable<Kanban>> GetByUserAsync(Guid userId);
    Task<Kanban?> CreateAsync(Kanban kanban);
    Task<bool> UpdateAsync(Kanban kanban);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddMemberAsync(KanbanMember member);
    Task<bool> RemoveMemberAsync(Guid kanbanId, Guid userId);
    Task<bool> IsMemberAsync(Guid kanbanId, Guid userId);
}
