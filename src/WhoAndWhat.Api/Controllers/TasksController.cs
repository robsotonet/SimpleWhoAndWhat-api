using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhoAndWhat.Application.Features.Tasks.Commands;
using WhoAndWhat.Application.Features.Tasks.Queries;

namespace WhoAndWhat.Api.Controllers;

public static class TasksController
{
    public static void MapTasksEndpoints(this WebApplication app)
    {
        var tasksGroup = app.MapGroup("/tasks").WithTags("Tasks");

        tasksGroup.MapPost("/", async (CreateTaskCommand command, IMediator mediator) =>
        {
            var taskId = await mediator.Send(command);
            return Results.Created($"/tasks/{taskId}", new { TaskId = taskId });
        });

        tasksGroup.MapGet("/", async ([FromQuery] Guid userId, IMediator mediator) =>
        {
            var tasks = await mediator.Send(new GetAllTasksForUserQuery(userId));
            return Results.Ok(tasks);
        });

        tasksGroup.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var task = await mediator.Send(new GetTaskByIdQuery(id));
            return task != null ? Results.Ok(task) : Results.NotFound();
        });

        tasksGroup.MapPut("/{id}", async (Guid id, [FromBody] UpdateTaskCommand command, IMediator mediator) =>
        {
            if (id != command.Id)
            {
                return Results.BadRequest();
            }
            await mediator.Send(command);
            return Results.NoContent();
        });

        tasksGroup.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteTaskCommand(id));
            return Results.NoContent();
        });
    }
}
