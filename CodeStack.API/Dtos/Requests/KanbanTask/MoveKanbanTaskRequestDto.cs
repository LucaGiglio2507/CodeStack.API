using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.KanbanTask;

public class MoveKanbanTaskRequestDto
{
    [Required(ErrorMessage = "NewColumnId is required.")]
    public Guid NewColumnId { get; set; }
}
