using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Event;

public class UpdateEventRequestDto
{
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
    public string? Title { get; set; }

    [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters.")]
    public string? Description { get; set; }

    public DateTime? StartsAt { get; set; }
}
