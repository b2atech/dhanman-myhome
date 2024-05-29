using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.UpdateFloor;

public class UpdateFloorCommandValidator : AbstractValidator<UpdateFloorCommand>
{
    #region Constructor
    public UpdateFloorCommandValidator(IFloorRepository floorRepository)
    {
        RuleFor(b => b.Name).MustAsync(async (name, _) =>
        {
            return !string.IsNullOrEmpty(name);
        }).WithMessage("The Floor Name is required");
    }
    #endregion
}
