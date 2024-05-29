using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateUnits
{
    public class CreateUnitsCommand : ICommand<Result<EntityCreatedResponse>>
    {
        #region Properties
        public List<CreateUnitsRequest> UnitsList { get; set; }

        #endregion

        #region Constructor
        public CreateUnitsCommand(List<CreateUnitsRequest> unitsList) => UnitsList = unitsList;

        #endregion
    }
}
