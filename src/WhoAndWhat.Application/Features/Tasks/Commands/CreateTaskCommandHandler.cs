using MediatR;
using WhoAndWhat.Domain.Entities;
using WhoAndWhat.Infrastructure;

namespace WhoAndWhat.Application.Features.Tasks.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateTaskCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new AppTask
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            DueDate = request.DueDate
        };

        _context.AppTasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        return task.Id;
    }
}
