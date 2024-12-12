using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateMultipleUnits;

public class CreateMultipleUnitCommandValidator : AbstractValidator<CreateMultipleUnitCommand>
{
    #region Constructor
    public CreateMultipleUnitCommandValidator(IUnitRepository unitRepository)
    {
        RuleFor(c => c.UnitList)
            .NotEmpty()
            .WithMessage("At least one Unit Id is required");

       
    }

    #endregion
}
