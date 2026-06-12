using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Repositories;

public interface IKanbanColumnRepository
{
    Task<KanbanColumn?> GetByIdAsync(Guid id);
    Task<IEnumerable<KanbanColumn>> GetByKanbanAsync(Guid kanbanId);
    Task<KanbanColumn?> CreateAsync(KanbanColumn column);
    Task<bool> UpdateAsync(KanbanColumn column);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ReorderAsync(Guid kanbanId, List<Guid> orderedColumnIds);
    Task<bool> HasTasksAsync(Guid columnId);
}
