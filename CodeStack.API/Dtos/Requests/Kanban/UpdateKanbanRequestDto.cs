using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Kanban;

public class UpdateKanbanRequestDto
{
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string? Description { get; set; }

    [StringLength(500, ErrorMessage = "Icon URL must not exceed 500 characters.")]
    public string? IconUrl { get; set; }
}
