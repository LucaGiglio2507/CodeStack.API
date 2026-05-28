namespace CodeStack.Domain.Entities;

public class Message
{
  public Guid Id { get; set; }
  public required string Content { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? ReadAt { get; set; }

  public Guid Sender_Id { get; set; }
  public User Sender { get; set; } = null!;

  public Guid Receiver_Id { get; set; }
  public User Receiver { get; set; } = null!;

  public Guid Group_Id { get; set; }
  public Group Group { get; set; } = null!;
}
