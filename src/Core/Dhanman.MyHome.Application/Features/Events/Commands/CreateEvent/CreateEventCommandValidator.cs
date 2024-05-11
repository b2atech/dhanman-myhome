using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator(IEventRepository eventRepository)
    {
        RuleFor(c => c.Title).MustAsync(async (title, _) =>
        {
            return !string.IsNullOrEmpty(title);
        }).WithMessage("The title is required");

    }
}
