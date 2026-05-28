namespace CodeStack.Domain.Entities;

public class File
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Url { get; set; }
  public required string Mime_type { get; set; }
  public long Size_bytes { get; set; }
  public DateTime Uploaded_At { get; set; }

  public Guid User_Id { get; set; }
  public virtual User User { get; set; } = null!;

  public Guid? Folder_Id { get; set; }
  public Folder? Folder { get; set; }
}
