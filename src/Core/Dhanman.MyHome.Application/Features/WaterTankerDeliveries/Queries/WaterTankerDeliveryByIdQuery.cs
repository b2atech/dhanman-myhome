using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Queries;

public sealed class WaterTankerDeliveryByIdQuery : IQuery<Result<WaterTankerDeliveryResponse>>
{
    #region Properties    
    public int WaterTankerDeliveryId { get; set; }
    #endregion

    #region Constructors
    public WaterTankerDeliveryByIdQuery(int waterTankerDeliveryId)
    {
        WaterTankerDeliveryId = waterTankerDeliveryId;
    }
    #endregion
}
