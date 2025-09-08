namespace WhoAndWhat.Domain.Entities;

public class ShoppingListItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid TaskId { get; set; }
    public required string ItemName { get; set; }
    public int Quantity { get; set; } = 1;
    public bool IsCompleted { get; set; }

    // Navigation properties
    public Task? Task { get; set; }
}
