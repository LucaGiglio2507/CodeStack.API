namespace CodeStack.Domain.Entities;

public class KanbanColumn
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Color { get; set; }
    public int Order { get; set; }

    public Guid Kanban_Id { get; set; }
    public Kanban Kanban { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
