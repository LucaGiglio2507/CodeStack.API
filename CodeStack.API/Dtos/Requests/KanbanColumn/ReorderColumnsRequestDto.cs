using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.KanbanColumn;

public class ReorderColumnsRequestDto
{
    [Required(ErrorMessage = "OrderedColumnIds is required.")]
    [MinLength(1, ErrorMessage = "At least one column ID is required.")]
    public List<Guid> OrderedColumnIds { get; set; } = [];
}
