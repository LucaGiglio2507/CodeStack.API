namespace CodeStack.Domain.Entities;

public class Group
{
  public Guid Id { get; set; }

  public ICollection<User> Participants { get; set; } = new List<User>();
  public ICollection<Message> Messages { get; set; } = new List<Message>();

}
