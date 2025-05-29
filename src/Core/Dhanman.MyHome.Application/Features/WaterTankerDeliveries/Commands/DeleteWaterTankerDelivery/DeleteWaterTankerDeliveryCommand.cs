using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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
