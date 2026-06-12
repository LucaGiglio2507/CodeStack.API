using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Synthesis;

public class UpdateSynthesisRequestDto
{
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public string? Content { get; set; }
}
