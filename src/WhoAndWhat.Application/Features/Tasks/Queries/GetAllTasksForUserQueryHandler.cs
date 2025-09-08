using MediatR;
using Microsoft.EntityFrameworkCore;
using WhoAndWhat.Application.Features.Tasks.Dtos;
using WhoAndWhat.Infrastructure;

namespace WhoAndWhat.Application.Features.Tasks.Queries;

public class GetAllTasksForUserQueryHandler : IRequestHandler<GetAllTasksForUserQuery, IEnumerable<TaskDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllTasksForUserQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetAllTasksForUserQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _context.AppTasks
            .AsNoTracking()
            .Where(t => t.UserId == request.UserId)
            .Select(task => new TaskDto(
                task.Id,
                task.Title,
                task.Description,
                task.Category,
                task.IsCompleted,
                task.DueDate,
                task.CreatedAt,
                task.UpdatedAt))
            .ToListAsync(cancellationToken);

        return tasks;
    }
}
