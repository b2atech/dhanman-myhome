using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.CreateFloor;

public class CreateFloorCommandValidator : AbstractValidator<CreateFloorCommand>
{
    public CreateFloorCommandValidator(IBuildingRepository buildingRepository)
    {
        RuleFor(c => c.Name)
        .NotEmpty().WithMessage("The Name is required");

        RuleFor(c => c.ApartmentId)
           .NotEmpty().WithMessage("The Apartment Id is required");

        RuleFor(c => c.BuildingId)
            .NotEmpty().WithMessage("The Building Id is required");

        RuleFor(c => c.TotalUnits)
            .NotEmpty().WithMessage("The Total Units are required")
            .GreaterThanOrEqualTo(0).WithMessage("The Total Units must be greater than zero");
    }
}
