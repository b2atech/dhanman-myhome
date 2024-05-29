using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.UpdateGate;

public  class UpdateGateCommandValidator : AbstractValidator<UpdateGateCommand>
{
    #region Constructor
    public UpdateGateCommandValidator(IGateRepository gateRepository)
    {
        RuleFor(b => b.Name).MustAsync(async (name, _) =>
        {
            return !string.IsNullOrEmpty(name);
        }).WithMessage("The Gate Name is required");
    }
    #endregion
}
