using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateMultipleUnits;

public class CreateMultipleUnitCommandValidator : AbstractValidator<CreateMultipleUnitCommand>
{
    #region Constructor
    public CreateMultipleUnitCommandValidator(IUnitRepository unitRepository)
    {
       

       
    }

    #endregion
}
