using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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
