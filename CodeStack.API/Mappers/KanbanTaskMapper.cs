using CodeStack.API.Dtos.Responses.KanbanTask;
using DomainTask = CodeStack.Domain.Entities.Task;

namespace CodeStack.API.Mappers;

public static class KanbanTaskMapper
{
    public static KanbanTaskResponseDto ToDto(DomainTask task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Priority = task.Priority,
        IsArchived = task.IsArchived,
        CreatedAt = task.Created_at,
        KanbanColumnId = task.KanbanColumn_Id,
        ColumnName = task.KanbanColumn?.Name ?? string.Empty,
        AssignedTo = task.AssignedTo is not null ? KanbanMemberMapper.ToDto(task.AssignedTo) : null,
        Tags = task.Tags.Select(TagMapper.ToDto)
    };

    public static IEnumerable<KanbanTaskResponseDto> ToDtoList(IEnumerable<DomainTask> tasks)
        => tasks.Select(ToDto);
}
