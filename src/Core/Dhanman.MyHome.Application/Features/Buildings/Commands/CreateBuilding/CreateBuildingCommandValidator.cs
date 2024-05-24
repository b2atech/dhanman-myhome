using Dhanman.MyHome.Application.Features.Buildings.Commands.CreateBuildings;
using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.CreateBuilding;

public class CreateBuildingCommandValidator : AbstractValidator<CreateBuildingCommand>
{
    public CreateBuildingCommandValidator(IBuildingRepository buildingRepository)
    {
        RuleFor(c => c.Name)
        .NotEmpty().WithMessage("The Name is required");

        RuleFor(c => c.BuildingTypeId)
            .NotEmpty().WithMessage("The Building Type Id is required");

        RuleFor(c => c.ApartmentId)
            .NotEmpty().WithMessage("The Apartment Id is required");

        RuleFor(c => c.TotalUnits)
            .NotEmpty().WithMessage("The Total Units are required")
            .GreaterThanOrEqualTo(0).WithMessage("The Total Units must be greater than zero");
    }
}
