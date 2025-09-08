using MediatR;
using WhoAndWhat.Domain.Enums;

namespace WhoAndWhat.Application.Features.Tasks.Commands;

public record CreateTaskCommand(
    Guid UserId,
    string Title,
    string? Description,
    TaskCategory Category,
    DateTime? DueDate) : IRequest<Guid>;
