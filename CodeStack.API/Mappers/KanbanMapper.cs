using CodeStack.API.Dtos.Responses.Kanban;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class KanbanMapper
{
    public static KanbanResponseDto ToDto(Kanban kanban) => new()
    {
        Id = kanban.Id,
        Name = kanban.Name,
        Description = kanban.Description,
        IconUrl = kanban.Icon_url,
        CreatedAt = kanban.Created_At,
        Creator = KanbanMemberMapper.ToDto(kanban.Creator),
        Members = kanban.Members.Select(KanbanMemberMapper.ToDto),
        Columns = KanbanColumnMapper.ToDtoList(kanban.Columns)
    };

    public static IEnumerable<KanbanResponseDto> ToDtoList(IEnumerable<Kanban> kanbans)
        => kanbans.Select(ToDto);
}
