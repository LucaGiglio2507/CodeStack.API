using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// CRUD for columns within a Kanban board; auto-assigns order on creation, prevents deleting non-empty columns, and persists drag-and-drop reordering.
/// </summary>
public class KanbanColumnService(IKanbanColumnRepository _columnRepository) : IKanbanColumnService
{
    public async Task<IEnumerable<KanbanColumn>> GetByKanbanAsync(Guid kanbanId)
    {
        return await _columnRepository.GetByKanbanAsync(kanbanId);
    }

    public async Task<KanbanColumn?> CreateAsync(Guid kanbanId, string name, string? color)
    {
        IEnumerable<KanbanColumn> columns = await _columnRepository.GetByKanbanAsync(kanbanId);
        int nextOrder = columns.Any() ? columns.Max(c => c.Order) + 1 : 0;

        KanbanColumn column = new KanbanColumn
        {
            Id = Guid.NewGuid(),
            Name = name,
            Color = color,
            Order = nextOrder,
            Kanban_Id = kanbanId
        };
        return await _columnRepository.CreateAsync(column);
    }

    public async Task<bool> UpdateAsync(Guid id, string? name, string? color)
    {
        KanbanColumn? column = await _columnRepository.GetByIdAsync(id);
        if (column is null) return false;

        if (name is not null) column.Name = name;
        if (color is not null) column.Color = color;

        return await _columnRepository.UpdateAsync(column);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        bool hasTasks = await _columnRepository.HasTasksAsync(id);
        if (hasTasks) throw new InvalidOperationException("Cannot delete a column that still contains tasks.");
        return await _columnRepository.DeleteAsync(id);
    }

    public async Task<bool> ReorderAsync(Guid kanbanId, List<Guid> orderedColumnIds)
    {
        return await _columnRepository.ReorderAsync(kanbanId, orderedColumnIds);
    }
}
