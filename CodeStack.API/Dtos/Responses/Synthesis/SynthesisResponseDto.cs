namespace CodeStack.API.Dtos.Responses.Synthesis;

public class SynthesisResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Content { get; set; }
    public bool IsSnippet { get; set; }
    public bool Archived { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid? FolderId { get; set; }
}
