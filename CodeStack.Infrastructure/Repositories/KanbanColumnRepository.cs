using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Domain.Entities;
using CodeStack.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeStack.Infrastructure.Repositories;

/// <summary>
/// EF Core persistence for KanbanColumn; eager-loads Tasks and returns columns ordered by Order. Supports bulk reorder and a HasTasks guard for safe deletion.
/// </summary>
public class KanbanColumnRepository(CodeStackDBContext _context) : IKanbanColumnRepository
{
    public async Task<KanbanColumn?> GetByIdAsync(Guid id)
    {
        return await _context.KanbanColumns
            .Include(c => c.Tasks)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<KanbanColumn>> GetByKanbanAsync(Guid kanbanId)
    {
        return await _context.KanbanColumns
            .Include(c => c.Tasks)
            .Where(c => c.Kanban_Id == kanbanId)
            .OrderBy(c => c.Order)
            .ToListAsync();
    }

    public async Task<KanbanColumn?> CreateAsync(KanbanColumn column)
    {
        _context.KanbanColumns.Add(column);
        await _context.SaveChangesAsync();
        return column;
    }

    public async Task<bool> UpdateAsync(KanbanColumn column)
    {
        _context.KanbanColumns.Update(column);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        KanbanColumn? column = await _context.KanbanColumns.FindAsync(id);
        if (column is null) return false;
        _context.KanbanColumns.Remove(column);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> ReorderAsync(Guid kanbanId, List<Guid> orderedColumnIds)
    {
        List<KanbanColumn> columns = await _context.KanbanColumns
            .Where(c => c.Kanban_Id == kanbanId)
            .ToListAsync();

        for (int i = 0; i < orderedColumnIds.Count; i++)
        {
            KanbanColumn? column = columns.FirstOrDefault(c => c.Id == orderedColumnIds[i]);
            if (column is not null) column.Order = i;
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> HasTasksAsync(Guid columnId)
    {
        return await _context.Tasks.AnyAsync(t => t.KanbanColumn_Id == columnId);
    }
}
