using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Event;

public class CreateEventRequestDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
    public string Title { get; set; } = null!;

    [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "StartsAt is required.")]
    public DateTime StartsAt { get; set; }
}
