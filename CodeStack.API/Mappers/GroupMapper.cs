using CodeStack.API.Dtos.Responses.Group;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class GroupMapper
{
    public static GroupResponseDto ToDto(Group group) => new()
    {
        Id = group.Id,
        Name = group.Name,
        CreatedAt = group.CreatedAt,
        Participants = group.Participants.Select(p => new GroupParticipantDto
        {
            Id = p.Id,
            Name = $"{p.First_name} {p.Name}",
            AvatarUrl = p.Avatar_Url
        })
    };

    public static IEnumerable<GroupResponseDto> ToDtoList(IEnumerable<Group> groups)
        => groups.Select(ToDto);
}
