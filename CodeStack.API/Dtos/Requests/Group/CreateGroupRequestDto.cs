using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Group;

public class CreateGroupRequestDto
{
    public string? Name { get; set; }

    [Required]
    public List<Guid> ParticipantIds { get; set; } = [];
}
