using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateMultipleUnits;

public class CreateMultipleUnitCommand : ICommand<Result<List<EntityCreatedResponse>>>
{
    #region Properties
    public List<UnitRequest> UnitList { get; set; }

    #endregion

    #region Constructor
    public CreateMultipleUnitCommand(List<UnitRequest> unitList) => UnitList = unitList;

    #endregion
}