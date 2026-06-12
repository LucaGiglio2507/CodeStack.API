namespace CodeStack.API.Dtos.Responses.Event;

public class EventResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Starts_At { get; set; }
}
