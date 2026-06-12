using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using DomainTask = CodeStack.Domain.Entities.Task;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for Task; eager-loads KanbanColumn, AssignedTo and Tags. Supports inter-column move by updating KanbanColumn_Id.
/// </summary>
public class KanbanTaskRepository(CodeStackDBContext _context) : IKanbanTaskRepository
{
    public async Task<DomainTask?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.KanbanColumn)
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<DomainTask>> GetByKanbanAsync(Guid kanbanId)
    {
        return await _context.Tasks
            .Include(t => t.KanbanColumn)
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .Where(t => t.KanbanColumn.Kanban_Id == kanbanId)
            .ToListAsync();
    }

    public async Task<IEnumerable<DomainTask>> GetByColumnAsync(Guid columnId)
    {
        return await _context.Tasks
            .Include(t => t.AssignedTo)
            .Include(t => t.Tags)
            .Where(t => t.KanbanColumn_Id == columnId)
            .ToListAsync();
    }

    public async Task<DomainTask?> CreateAsync(DomainTask task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateAsync(DomainTask task)
    {
        _context.Tasks.Update(task);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> MoveAsync(Guid taskId, Guid newColumnId)
    {
        DomainTask? task = await _context.Tasks.FindAsync(taskId);
        if (task is null) return false;
        task.KanbanColumn_Id = newColumnId;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        DomainTask? task = await _context.Tasks.FindAsync(id);
        if (task is null) return false;
        _context.Tasks.Remove(task);
        return await _context.SaveChangesAsync() > 0;
    }
}
