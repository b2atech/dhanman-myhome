using Dhanman.MyHome.Domain.Abstractions;
using FluentValidation;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.DeleteBuilding;

public class DeleteBuildingCommandValidator : AbstractValidator<DeleteBuildingCommand>
{
    #region Constructor
    public DeleteBuildingCommandValidator(IBuildingRepository buildingRepository)
    {
       
    }
    #endregion
}
