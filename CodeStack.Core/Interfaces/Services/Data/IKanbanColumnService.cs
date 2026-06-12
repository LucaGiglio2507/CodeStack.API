using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface IKanbanColumnService
{
    Task<IEnumerable<KanbanColumn>> GetByKanbanAsync(Guid kanbanId);
    Task<KanbanColumn?> CreateAsync(Guid kanbanId, string name, string? color);
    Task<bool> UpdateAsync(Guid id, string? name, string? color);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ReorderAsync(Guid kanbanId, List<Guid> orderedColumnIds);
}
