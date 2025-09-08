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
    public DbSet<AppTask> AppTasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User has many AppTasks
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        // AppTask has many Subtasks
        modelBuilder.Entity<AppTask>()
            .HasMany(t => t.Subtasks)
            .WithOne(s => s.AppTask)
            .HasForeignKey(s => s.TaskId);

        // AppTask has many ShoppingListItems
        modelBuilder.Entity<AppTask>()
            .HasMany(t => t.ShoppingListItems)
            .WithOne(sli => sli.AppTask)
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
