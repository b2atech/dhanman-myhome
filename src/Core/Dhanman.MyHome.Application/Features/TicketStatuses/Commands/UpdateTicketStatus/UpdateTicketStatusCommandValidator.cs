using FluentValidation;

namespace Dhanman.MyHome.Application.Features.TicketStatuses.Commands.UpdateTicketNextStatus;

public class UpdateTicketStatusCommandValidator : AbstractValidator<UpdateTicketStatusCommand>
{
    public UpdateTicketStatusCommandValidator()
    {
        RuleFor(c => c.TicketIds)
            .NotEmpty()
            .WithMessage("At least one TicketId is required");

        RuleFor(c => c.TicketStatusId)
            .GreaterThan(0)
            .WithMessage("Invalid Ticket Status ID");
    }
}
