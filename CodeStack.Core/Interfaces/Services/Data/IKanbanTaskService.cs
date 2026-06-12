using CodeStack.Domain.Enums;
using DomainTask = CodeStack.Domain.Entities.Task;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface IKanbanTaskService
{
    Task<DomainTask?> GetByIdAsync(Guid id);
    Task<IEnumerable<DomainTask>> GetByKanbanAsync(Guid kanbanId);
    Task<DomainTask?> CreateAsync(Guid columnId, string title, string? description, TaskPriority priority, Guid? assigneeId, List<Guid>? tagIds);
    Task<bool> UpdateAsync(Guid id, string? title, string? description, TaskPriority? priority, Guid? assigneeId);
    Task<bool> MoveAsync(Guid taskId, Guid newColumnId);
    Task<bool> DeleteAsync(Guid id);
}
