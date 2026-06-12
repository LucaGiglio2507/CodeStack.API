using CodeStack.Domain.Enums;

namespace CodeStack.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; }
    public bool IsArchived { get; set; }
    public DateTime Created_at { get; set; }

    public Guid KanbanColumn_Id { get; set; }
    public KanbanColumn KanbanColumn { get; set; } = null!;

    public Guid? User_Id { get; set; }
    public User? AssignedTo { get; set; }

    public ICollection<Tag> Tags { get; set; } = [];
}
