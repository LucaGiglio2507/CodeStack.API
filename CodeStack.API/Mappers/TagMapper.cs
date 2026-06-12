using CodeStack.API.Dtos.Responses.KanbanTask;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class TagMapper
{
    public static TagResponseDto ToDto(Tag tag) => new()
    {
        Id = tag.Id,
        Name = tag.Name,
        Color = tag.Color
    };

    public static IEnumerable<TagResponseDto> ToDtoList(IEnumerable<Tag> tags)
        => tags.Select(ToDto);
}
