using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateUnit;

public class CreateUnitCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public List<CreateUnitRequest> UnitList { get; set; }

    #endregion

    #region Constructor
    public CreateUnitCommand(List<CreateUnitRequest> unitList) => UnitList = unitList;

    #endregion
}
