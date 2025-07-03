using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands.DeleteWaterTankerDelivery;

public sealed class DeleteWaterTankerDeliveryCommand : ICommand<Result<EntityDeletedResponse>>
{
    #region Properties
    public int DeleteWaterTankerDeliveryId { get; }
    #endregion

    #region Constructor

    public DeleteWaterTankerDeliveryCommand(int deleteWaterTankerDeliveryId) => DeleteWaterTankerDeliveryId = deleteWaterTankerDeliveryId;
    #endregion
}
