using MediatR;
using Microsoft.EntityFrameworkCore;
using WhoAndWhat.Application.Features.Tasks.Dtos;
using WhoAndWhat.Infrastructure;

namespace WhoAndWhat.Application.Features.Tasks.Queries;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
{
    private readonly ApplicationDbContext _context;

    public GetTaskByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _context.AppTasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (task == null)
        {
            return null;
        }

        return new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.Category,
            task.IsCompleted,
            task.DueDate,
            task.CreatedAt,
            task.UpdatedAt);
    }
}
