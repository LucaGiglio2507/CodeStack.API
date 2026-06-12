using CodeStack.API.Dtos.Responses.Kanban;
using CodeStack.Domain.Enums;

namespace CodeStack.API.Dtos.Responses.KanbanTask;

public class KanbanTaskResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid KanbanColumnId { get; set; }
    public string ColumnName { get; set; } = null!;
    public KanbanMemberDto? AssignedTo { get; set; }
    public IEnumerable<TagResponseDto> Tags { get; set; } = [];
}
