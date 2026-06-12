using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Group;

public class AddParticipantRequestDto
{
    [Required]
    public Guid UserId { get; set; }
}
