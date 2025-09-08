namespace WhoAndWhat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid SenderId { get; set; }
    public required Guid ReceiverId { get; set; }
    public required string Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }
    public DateTime? ExpiresAt { get; set; }

    // Navigation properties
    public User? Sender { get; set; }
    public User? Receiver { get; set; }
}
