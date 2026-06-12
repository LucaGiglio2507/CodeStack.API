using DomainTask = CodeStack.Domain.Entities.Task;

namespace CodeStack.Core.Interfaces.Repositories;

public interface IKanbanTaskRepository
{
    Task<DomainTask?> GetByIdAsync(Guid id);
    Task<IEnumerable<DomainTask>> GetByKanbanAsync(Guid kanbanId);
    Task<IEnumerable<DomainTask>> GetByColumnAsync(Guid columnId);
    Task<DomainTask?> CreateAsync(DomainTask task);
    Task<bool> UpdateAsync(DomainTask task);
    Task<bool> MoveAsync(Guid taskId, Guid newColumnId);
    Task<bool> DeleteAsync(Guid id);
}
