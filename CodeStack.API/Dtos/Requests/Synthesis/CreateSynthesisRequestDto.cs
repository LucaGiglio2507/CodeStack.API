using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Synthesis;

public class CreateSynthesisRequestDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public string? Content { get; set; }

    public bool IsSnippet { get; set; }
}
