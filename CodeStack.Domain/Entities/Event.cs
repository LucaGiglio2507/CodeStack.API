namespace CodeStack.Domain.Entities;

public class Event
{
  public Guid Id { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public DateTime Starts_At { get; set; }

  public Guid User_Id { get; set; }
  public  User User { get; set; } = null!;
}
