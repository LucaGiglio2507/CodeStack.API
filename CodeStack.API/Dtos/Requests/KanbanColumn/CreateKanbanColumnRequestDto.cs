using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.KanbanColumn;

public class CreateKanbanColumnRequestDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Color must not exceed 50 characters.")]
    public string? Color { get; set; }
}
