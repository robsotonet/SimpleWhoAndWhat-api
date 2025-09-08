using MediatR;
using Microsoft.EntityFrameworkCore;
using WhoAndWhat.Infrastructure;

namespace WhoAndWhat.Application.Features.Tasks.Commands;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateTaskCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.AppTasks.FindAsync(request.Id, cancellationToken);

        if (task == null)
        {
            // Or throw a custom not found exception
            return;
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.Category = request.Category;
        task.IsCompleted = request.IsCompleted;
        task.DueDate = request.DueDate;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
