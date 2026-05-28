namespace CodeStack.Domain.Entities;

public class Kanban
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public string? Icon_url { get; set; }
  public DateTime Created_At { get; set; }

  public Guid Creator_Id { get; set; }
  public User Creator { get; set; } = null!;

  public ICollection<KanbanMember> Members { get; set; } = new List<KanbanMember>();
  public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
