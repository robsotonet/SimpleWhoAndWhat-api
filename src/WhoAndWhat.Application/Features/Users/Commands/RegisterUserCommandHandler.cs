using MediatR;
using WhoAndWhat.Domain.Entities;

namespace WhoAndWhat.Application.Features.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    // private readonly IUserRepository _userRepository; // TODO: Inject repository when available

    public RegisterUserCommandHandler(/*IUserRepository userRepository*/)
    {
        // _userRepository = userRepository;
    }

    public Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        // TODO: Persist user to database using the repository
        // await _userRepository.AddAsync(user, cancellationToken);

        return Task.FromResult(user.Id);
    }
}
