namespace WhoAndWhat.Domain.Entities;

public class Subtask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid TaskId { get; set; }
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }

    // Navigation properties
    public Task? Task { get; set; }
}
