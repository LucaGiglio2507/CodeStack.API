namespace CodeStack.API.Dtos.Responses.KanbanTask;

public class TagResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;
}
