using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Message;

public class SendMessageRequestDto
{
    [Required]
    public string Content { get; set; } = null!;
}
