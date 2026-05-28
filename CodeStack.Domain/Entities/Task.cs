namespace CodeStack.Domain.Entities;

public class Task
{
  public Guid Id { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public bool IsArchived { get; set; }
  public DateTime Created_at { get; set; }

  public Guid Kanban_Id { get; set; }
  public virtual Kanban Kanban { get; set; } = null!;

  public Guid User_Id { get; set; }
  public User AssignedTo { get; set; } = null!;

  public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
