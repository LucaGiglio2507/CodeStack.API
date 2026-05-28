namespace CodeStack.Domain.Entities;

public class Tag
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Color { get; set; }

  public ICollection<Task> Tasks { get; set; } = new List<Task>();
  public ICollection<Folder> Folders { get; set; } = new List<Folder>();
}
