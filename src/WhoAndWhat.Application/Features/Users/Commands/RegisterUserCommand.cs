using MediatR;

namespace WhoAndWhat.Application.Features.Users.Commands;

public record RegisterUserCommand(
    string Email,
    string Password
) : IRequest<Guid>;
