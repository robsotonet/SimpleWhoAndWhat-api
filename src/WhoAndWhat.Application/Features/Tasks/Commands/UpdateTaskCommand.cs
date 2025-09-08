using MediatR;
using WhoAndWhat.Domain.Enums;

namespace WhoAndWhat.Application.Features.Tasks.Commands;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string? Description,
    TaskCategory Category,
    bool IsCompleted,
    DateTime? DueDate) : IRequest;
