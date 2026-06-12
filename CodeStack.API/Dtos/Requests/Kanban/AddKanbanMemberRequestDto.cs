using System.ComponentModel.DataAnnotations;
using CodeStack.Domain.Enums;

namespace CodeStack.API.Dtos.Requests.Kanban;

public class AddKanbanMemberRequestDto
{
    [Required(ErrorMessage = "UserId is required.")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public Roles Role { get; set; }
}
