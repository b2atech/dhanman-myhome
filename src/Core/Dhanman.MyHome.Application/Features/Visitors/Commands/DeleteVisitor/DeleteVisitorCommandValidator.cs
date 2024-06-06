using Dhanman.MyHome.Application.Features.Visitors.Commands.DeleteVisitor;
using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Visitors.Features.DeleteVisitor.Commands.DeleteVisitor;

public class DeleteVisitorCommandValidator : AbstractValidator<DeleteVisitorCommand>
{

    #region Constructor
    public DeleteVisitorCommandValidator(IUnitRepository unitRepository)
    {

    }
    #endregion
}
