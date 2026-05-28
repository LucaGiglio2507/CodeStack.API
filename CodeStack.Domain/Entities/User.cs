using CodeStack.Domain.Enums;

namespace CodeStack.Domain.Entities;

public class User
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string First_name { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required Roles Role { get; set; }
  public string? Avatar_Url { get; set; }
  public DateTime Created_At { get; set; }
  public DateTime? Last_Login { get; set; }
  public bool IsActive { get; set; }
  public bool IsActivated { get; set; }
  public bool CookieAccepted { get; set;  }

  public  ICollection<Kanban> CreatedKanbans { get; set; } = new List<Kanban>();
  public  ICollection<KanbanMember> KanbanMemberships { get; set; } = new List<KanbanMember>();
  public  ICollection<Event> Events { get; set; } = new List<Event>();
  public  ICollection<Synthesis> Syntheses { get; set; } = new List<Synthesis>();
  public  ICollection<File> Files { get; set; } = new List<File>();
  public  ICollection<Folder> Folders { get; set; } = new List<Folder>();
  public  ICollection<Message> SentMessages { get; set; } = new List<Message>();
  public  ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
  public  ICollection<Group> Groups { get; set; } = new List<Group>();
}
}
