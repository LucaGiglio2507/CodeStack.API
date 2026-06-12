using CodeStack.API.Dtos.Responses.KanbanColumn;

namespace CodeStack.API.Dtos.Responses.Kanban;

public class KanbanResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public KanbanMemberDto Creator { get; set; } = null!;
    public IEnumerable<KanbanMemberDto> Members { get; set; } = [];
    public IEnumerable<KanbanColumnResponseDto> Columns { get; set; } = [];
}
