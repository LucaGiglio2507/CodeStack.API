using System.ComponentModel.DataAnnotations;
using CodeStack.Domain.Enums;

namespace CodeStack.API.Dtos.Requests.KanbanTask;

public class CreateKanbanTaskRequestDto
{
    [Required(ErrorMessage = "ColumnId is required.")]
    public Guid ColumnId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
    public string Title { get; set; } = null!;

    [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Priority is required.")]
    public TaskPriority Priority { get; set; }

    public Guid? AssigneeId { get; set; }

    public List<Guid>? TagIds { get; set; }
}
