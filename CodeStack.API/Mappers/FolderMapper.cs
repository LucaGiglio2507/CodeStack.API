using CodeStack.API.Dtos.Responses.Folder;
using CodeStack.API.Dtos.Responses.KanbanTask;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class FolderMapper
{
    public static FolderResponseDto ToDto(Folder folder) => new()
    {
        Id = folder.Id,
        Title = folder.Title,
        Description = folder.Description,
        Icon = folder.Icon,
        Color = folder.Color,
        Archived = folder.Archived,
        Created_At = folder.Created_At,
        Parent_Folder_Id = folder.Parent_Folder_Id,
        SubFolders = folder.SubFolders.Select(ToDto),
        Tags = folder.Tags.Select(t => new TagResponseDto { Id = t.Id, Name = t.Name, Color = t.Color })
    };

    public static IEnumerable<FolderResponseDto> ToDtoList(IEnumerable<Folder> folders)
        => folders.Select(ToDto);
}
