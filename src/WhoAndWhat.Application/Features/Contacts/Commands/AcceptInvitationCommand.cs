using MediatR;

namespace WhoAndWhat.Application.Features.Contacts.Commands;

public record AcceptInvitationCommand(Guid UserId, string InvitationCode) : IRequest;
