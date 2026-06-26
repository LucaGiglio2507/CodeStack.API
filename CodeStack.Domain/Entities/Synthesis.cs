namespace CodeStack.Domain.Entities;

public class Synthesis
{
  public Guid Id { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public string? Content { get; set; }
  public bool Archived { get; set; }
  public bool IsSnippet { get; set; }
  public DateTime Created_At { get; set; }

  public Guid User_Id { get; set; }
  public User User { get; set; } = null!;

  public Guid? FolderId { get; set; }
  public Folder? Folder { get; set; }
}
