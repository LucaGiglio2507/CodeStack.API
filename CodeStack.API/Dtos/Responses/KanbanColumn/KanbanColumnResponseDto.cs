namespace CodeStack.API.Dtos.Responses.KanbanColumn;

public class KanbanColumnResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Color { get; set; }
    public int Order { get; set; }
    public int TaskCount { get; set; }
}
