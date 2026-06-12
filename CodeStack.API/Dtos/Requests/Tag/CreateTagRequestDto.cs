using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Tag;

public class CreateTagRequestDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Color is required.")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be a valid hex color (e.g. #FF5733).")]
    public string Color { get; set; } = null!;
}
