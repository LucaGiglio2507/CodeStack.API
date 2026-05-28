using CodeStack.Domain.Enums;

namespace CodeStack.Domain.Entities;

public class KanbanMember
{
  public Guid Id { get; set; }
  public required Roles Role { get; set; }
  public DateTime Joined_At { get; set; }

  public Guid User_Id { get; set; }
  public User User { get; set; } = null!;

  public Guid Kanban_Id { get; set; }
  public Kanban Kanban { get; set; } = null!;
}
