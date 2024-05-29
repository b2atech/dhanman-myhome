using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.DeleteGate;

public class DeleteGateCommandValidator : AbstractValidator<DeleteGateCommand>
{

    #region Constructor
    public DeleteGateCommandValidator(IUnitRepository unitRepository)
    {

    }
    #endregion
}
