using MediatR;

namespace WhoAndWhat.Application.Features.Tasks.Commands;

public record DeleteTaskCommand(Guid Id) : IRequest;
