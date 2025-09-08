using MediatR;
using WhoAndWhat.Application.Features.Tasks.Dtos;

namespace WhoAndWhat.Application.Features.Tasks.Queries;

public record GetAllTasksForUserQuery(Guid UserId) : IRequest<IEnumerable<TaskDto>>;
