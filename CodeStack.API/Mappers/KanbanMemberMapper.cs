using CodeStack.API.Dtos.Responses.Kanban;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class KanbanMemberMapper
{
    public static KanbanMemberDto ToDto(User user) => new()
    {
        Id = user.Id,
        Name = $"{user.First_name} {user.Name}",
        Email = user.Email,
        AvatarUrl = user.Avatar_Url
    };

    public static KanbanMemberDto ToDto(KanbanMember member) => new()
    {
        Id = member.User.Id,
        Name = $"{member.User.First_name} {member.User.Name}",
        Email = member.User.Email,
        AvatarUrl = member.User.Avatar_Url
    };
}
