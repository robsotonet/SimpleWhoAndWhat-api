using MediatR;

namespace WhoAndWhat.Application.Features.Contacts.Commands;

// TODO: Implement QR code/short code generation
public record InviteContactCommand(Guid UserId) : IRequest<string>;
