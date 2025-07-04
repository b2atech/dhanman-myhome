using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Units.Command.DeleteUnit
{
    public class DeleteUnitCommand : ICommand<Result<EntityDeletedResponse>>
    {
        #region Properties
        public int UnitId { get; }

        #endregion

        #region Constructor

        public DeleteUnitCommand(int unitId) => UnitId = unitId;

        #endregion
    }
}
