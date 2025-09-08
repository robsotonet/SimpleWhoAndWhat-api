using WhoAndWhat.Domain.Enums;

namespace WhoAndWhat.Domain.Entities;

public class AppTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid UserId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TaskCategory Category { get; set; } = TaskCategory.Generic;
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User? User { get; set; }
    public ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
    public ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();
}
