using MediatR;
using Microsoft.EntityFrameworkCore;
using WhoAndWhat.Application.Features.Contacts.Dtos;
using WhoAndWhat.Infrastructure;

namespace WhoAndWhat.Application.Features.Contacts.Queries;

public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IEnumerable<ContactDto>>
{
    private readonly ApplicationDbContext _context;

    public GetContactsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _context.Contacts
            .AsNoTracking()
            .Where(c => c.UserId == request.UserId)
            .Select(c => new ContactDto(c.Id, c.ContactUserId))
            .ToListAsync(cancellationToken);

        return contacts;
    }
}
