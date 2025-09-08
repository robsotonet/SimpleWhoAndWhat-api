using MediatR;
using WhoAndWhat.Application.Features.Contacts.Dtos;

namespace WhoAndWhat.Application.Features.Contacts.Queries;

public record GetContactsQuery(Guid UserId) : IRequest<IEnumerable<ContactDto>>;
