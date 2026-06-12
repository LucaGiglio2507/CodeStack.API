using CodeStack.API.Dtos.Responses.KanbanColumn;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class KanbanColumnMapper
{
    public static KanbanColumnResponseDto ToDto(KanbanColumn column) => new()
    {
        Id = column.Id,
        Name = column.Name,
        Color = column.Color,
        Order = column.Order,
        TaskCount = column.Tasks.Count
    };

    public static IEnumerable<KanbanColumnResponseDto> ToDtoList(IEnumerable<KanbanColumn> columns)
        => columns.Select(ToDto);
}
