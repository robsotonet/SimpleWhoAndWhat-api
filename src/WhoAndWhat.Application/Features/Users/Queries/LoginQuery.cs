using MediatR;
using WhoAndWhat.Application.Features.Users.Dtos;

namespace WhoAndWhat.Application.Features.Users.Queries;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<AuthTokensDto>;
