using FluentValidation;

namespace WhoAndWhat.Application.Features.Tasks.Commands;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
