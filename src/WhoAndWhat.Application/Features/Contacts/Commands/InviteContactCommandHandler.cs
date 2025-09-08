using MediatR;

namespace WhoAndWhat.Application.Features.Contacts.Commands;

public class InviteContactCommandHandler : IRequestHandler<InviteContactCommand, string>
{
    public Task<string> Handle(InviteContactCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement a more secure and persistent invitation logic
        var invitationCode = $"INVITE_{request.UserId}_{Guid.NewGuid()}";
        return Task.FromResult(invitationCode);
    }
}
