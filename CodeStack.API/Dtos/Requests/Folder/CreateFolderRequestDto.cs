using System.ComponentModel.DataAnnotations;

namespace CodeStack.API.Dtos.Requests.Folder;

public class CreateFolderRequestDto
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
    public string Title { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string? Description { get; set; }

    [StringLength(100, ErrorMessage = "Icon must not exceed 100 characters.")]
    public string? Icon { get; set; }

    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be a valid hex color (e.g. #FF5733).")]
    public string? Color { get; set; }

    public Guid? Parent_Folder_Id { get; set; }
}
