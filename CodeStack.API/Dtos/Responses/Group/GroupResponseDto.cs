namespace CodeStack.API.Dtos.Responses.Group;

public class GroupResponseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<GroupParticipantDto> Participants { get; set; } = [];
}

public class GroupParticipantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? AvatarUrl { get; set; }
}
