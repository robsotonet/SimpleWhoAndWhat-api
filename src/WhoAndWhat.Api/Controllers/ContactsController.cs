using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhoAndWhat.Application.Features.Contacts.Commands;
using WhoAndWhat.Application.Features.Contacts.Queries;

namespace WhoAndWhat.Api.Controllers;

public static class ContactsController
{
    public static void MapContactsEndpoints(this WebApplication app)
    {
        var contactsGroup = app.MapGroup("/contacts").WithTags("Contacts");

        contactsGroup.MapPost("/invitations", async (InviteContactCommand command, IMediator mediator) =>
        {
            var invitationCode = await mediator.Send(command);
            return Results.Ok(new { InvitationCode = invitationCode });
        });

        contactsGroup.MapPost("/invitations/accept", async (AcceptInvitationCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });

        contactsGroup.MapGet("/", async ([FromQuery] Guid userId, IMediator mediator) =>
        {
            var contacts = await mediator.Send(new GetContactsQuery(userId));
            return Results.Ok(contacts);
        });
    }
}
