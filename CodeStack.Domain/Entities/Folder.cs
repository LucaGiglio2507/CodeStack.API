namespace CodeStack.Domain.Entities;

public class Folder
{
  public Guid Id { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public string? Icon { get; set; }
  public string? Color { get; set; }
  public bool Archived { get; set; }
  public DateTime Created_At { get; set; }

  public Guid User_Id { get; set; }
  public User User { get; set; } = null!;

  public Guid? Parent_Folder_Id { get; set; }
  public Folder? ParentFolder { get; set; }

  public ICollection<Folder> SubFolders { get; set; } = new List<Folder>();
  public ICollection<File> Files { get; set; } = new List<File>();
  public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
