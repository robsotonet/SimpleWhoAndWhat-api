using Microsoft.EntityFrameworkCore;
using WhoAndWhat.Domain.Entities;

namespace WhoAndWhat.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User has many Tasks
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        // Task has many Subtasks
        modelBuilder.Entity<Domain.Entities.Task>()
            .HasMany(t => t.Subtasks)
            .WithOne(s => s.Task)
            .HasForeignKey(s => s.TaskId);

        // Task has many ShoppingListItems
        modelBuilder.Entity<Domain.Entities.Task>()
            .HasMany(t => t.ShoppingListItems)
            .WithOne(sli => sli.Task)
            .HasForeignKey(sli => sli.TaskId);

        // Configure Message relationships
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
