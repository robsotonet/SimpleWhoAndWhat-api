using MediatR;
using Microsoft.EntityFrameworkCore;
using WhoAndWhat.Infrastructure;

namespace WhoAndWhat.Application.Features.Tasks.Commands;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteTaskCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.AppTasks.FindAsync(request.Id, cancellationToken);

        if (task == null)
        {
            // Or throw a custom not found exception
            return;
        }

        _context.AppTasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
