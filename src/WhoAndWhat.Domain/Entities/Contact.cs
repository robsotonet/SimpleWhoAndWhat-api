namespace WhoAndWhat.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid UserId { get; set; }
    public required Guid ContactUserId { get; set; }
    public bool IsBlocked { get; set; }

    // Navigation properties
    public User? User { get; set; }
}
