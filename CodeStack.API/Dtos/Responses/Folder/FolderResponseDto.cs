using CodeStack.API.Dtos.Responses.KanbanTask;

namespace CodeStack.API.Dtos.Responses.Folder;

public class FolderResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public bool Archived { get; set; }
    public DateTime Created_At { get; set; }
    public Guid? Parent_Folder_Id { get; set; }
    public IEnumerable<FolderResponseDto> SubFolders { get; set; } = [];
    public IEnumerable<TagResponseDto> Tags { get; set; } = [];
}
