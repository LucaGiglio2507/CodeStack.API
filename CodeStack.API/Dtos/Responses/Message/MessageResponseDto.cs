namespace CodeStack.API.Dtos.Responses.Message;

public class MessageResponseDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public Guid? GroupId { get; set; }
    public MessageSenderDto Sender { get; set; } = null!;
}

public class MessageSenderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? AvatarUrl { get; set; }
}
