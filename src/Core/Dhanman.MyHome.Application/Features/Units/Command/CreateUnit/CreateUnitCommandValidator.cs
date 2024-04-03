using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateUnit;

public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    #region Constructor
    public CreateUnitCommandValidator(IUnitRepository unitRepository)
    {
        RuleFor(c => c.UnitList)
            .NotEmpty()
            .WithMessage("At least one Flat Id is required");

        RuleForEach(c => c.UnitList)
            .MustAsync(async(unit, flat, context) =>
            {
                return await unitRepository.IsFlatValidAsync(flat.Flat);
            })
            .WithMessage("The flat id is invalid.");
    }

    #endregion
}
