using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands;
public sealed class CreateWaterTankerDeliveriesBulkCommand : ICommand<Result<EntityCreatedResponse>>
{
    public IReadOnlyList<CreateWaterTankerDeliveryDto> Deliveries { get; }

    public CreateWaterTankerDeliveriesBulkCommand(IEnumerable<CreateWaterTankerDeliveryDto> deliveries)
    {
        Deliveries = deliveries.ToList();
    }
}