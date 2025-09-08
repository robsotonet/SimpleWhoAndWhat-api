using MediatR;
using WhoAndWhat.Domain.Entities;
using WhoAndWhat.Infrastructure;

namespace WhoAndWhat.Application.Features.Contacts.Commands;

public class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand>
{
    private readonly ApplicationDbContext _context;

    public AcceptInvitationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement a proper invitation code validation logic
        var parts = request.InvitationCode.Split('_');
        if (parts.Length != 3 || parts[0] != "INVITE")
        {
            return;
        }

        if (!Guid.TryParse(parts[1], out var invitingUserId))
        {
            return;
        }

        var contact1 = new Contact
        {
            UserId = request.UserId,
            ContactUserId = invitingUserId
        };

        var contact2 = new Contact
        {
            UserId = invitingUserId,
            ContactUserId = request.UserId
        };

        _context.Contacts.Add(contact1);
        _context.Contacts.Add(contact2);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
