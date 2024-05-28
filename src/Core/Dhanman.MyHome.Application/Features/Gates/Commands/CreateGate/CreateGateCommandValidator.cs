using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.CreateGate;

public class CreateGateCommandValidator : AbstractValidator<CreateGateCommand>
{
    public CreateGateCommandValidator(IGateRepository gateRepository)
    {
        RuleFor(c => c.Name)
        .NotEmpty().WithMessage("The Name is required");

        RuleFor(c => c.ApartmentId)
            .NotEmpty().WithMessage("The Apartment Id is required");

        RuleFor(c => c.BuildingId)
            .NotEmpty().WithMessage("The Building Id is required")
            .GreaterThanOrEqualTo(0).WithMessage("The Building Id must be greater than zero");

        RuleFor(c => c.GateTypeId)
            .NotEmpty().WithMessage("The Gate Type Id are required")
            .GreaterThanOrEqualTo(0).WithMessage("The Total Units must be greater than zero");
    }
}