using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Enums;
using DomainTask = CodeStack.Domain.Entities.Task;
using KanbanColumn = CodeStack.Domain.Entities.KanbanColumn;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// CRUD for Kanban tasks plus inter-column move; validates the target column exists before moving.
/// </summary>
public class KanbanTaskService(
    IKanbanTaskRepository _taskRepository,
    IKanbanColumnRepository _columnRepository) : IKanbanTaskService
{
    public async Task<DomainTask?> GetByIdAsync(Guid id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<DomainTask>> GetByKanbanAsync(Guid kanbanId)
    {
        return await _taskRepository.GetByKanbanAsync(kanbanId);
    }

    public async Task<DomainTask?> CreateAsync(Guid columnId, string title, string? description, TaskPriority priority, Guid? assigneeId, List<Guid>? tagIds)
    {
        DomainTask task = new DomainTask
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Priority = priority,
            KanbanColumn_Id = columnId,
            User_Id = assigneeId,
            Created_at = DateTime.UtcNow
        };
        return await _taskRepository.CreateAsync(task);
    }

    public async Task<bool> UpdateAsync(Guid id, string? title, string? description, TaskPriority? priority, Guid? assigneeId)
    {
        DomainTask? task = await _taskRepository.GetByIdAsync(id);
        if (task is null) return false;

        if (title is not null) task.Title = title;
        if (description is not null) task.Description = description;
        if (priority is not null) task.Priority = priority.Value;
        if (assigneeId is not null) task.User_Id = assigneeId;

        return await _taskRepository.UpdateAsync(task);
    }

    public async Task<bool> MoveAsync(Guid taskId, Guid newColumnId)
    {
        KanbanColumn? column = await _columnRepository.GetByIdAsync(newColumnId);
        if (column is null) throw new KeyNotFoundException("Target column not found.");
        return await _taskRepository.MoveAsync(taskId, newColumnId);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _taskRepository.DeleteAsync(id);
    }
}
