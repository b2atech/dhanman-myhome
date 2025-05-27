using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands;
public sealed class CreateWaterTankerDeliveriesBulkCommand : ICommand<Result<EntityCreatedResponse>>
{
    public IReadOnlyList<CreateWaterTankerDeliveryDto> Deliveries { get; }
   // public Guid CreatedBy { get; }
   // public DateTime CreatedOnUtc { get; }

    public CreateWaterTankerDeliveriesBulkCommand(IEnumerable<CreateWaterTankerDeliveryDto> deliveries)
    {
        Deliveries = deliveries.ToList();
    //    CreatedBy = createdBy;
    //    CreatedOnUtc = createdOnUtc;
    }
}