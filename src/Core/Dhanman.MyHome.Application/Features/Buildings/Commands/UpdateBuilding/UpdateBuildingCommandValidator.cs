using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.UpdateBuilding;

public class UpdateBuildingCommandValidator : AbstractValidator<UpdateBuildingCommand>
{
    #region Constructor
    public UpdateBuildingCommandValidator(IBuildingRepository buildingRepository)
    {
        RuleFor(b => b.Name).MustAsync(async (name, _) =>
        {
            return !string.IsNullOrEmpty(name);
        }).WithMessage("The Building Name is required");
    }
    #endregion
}
