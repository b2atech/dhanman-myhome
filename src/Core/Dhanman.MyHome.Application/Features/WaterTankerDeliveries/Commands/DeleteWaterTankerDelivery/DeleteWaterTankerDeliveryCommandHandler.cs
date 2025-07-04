using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Event;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands.DeleteWaterTankerDelivery;

public sealed class DeleteWaterTankerDeliveryCommandHandler : ICommandHandler<DeleteWaterTankerDeliveryCommand, Result<EntityDeletedResponse>>
{
    #region Properties

    private readonly IWaterTankerDeliveryRepository _waterTankerDeliveryRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public DeleteWaterTankerDeliveryCommandHandler(IWaterTankerDeliveryRepository waterTankerDeliveryRepository, IMediator mediator)
    {
        _waterTankerDeliveryRepository = waterTankerDeliveryRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityDeletedResponse>> Handle(DeleteWaterTankerDeliveryCommand request, CancellationToken cancellationToken)
    {
        var updateWaterTankerDeliveries = await _waterTankerDeliveryRepository.GetByIntIdAsync(request.DeleteWaterTankerDeliveryId);

        if (updateWaterTankerDeliveries == null)
        {
            throw new WaterTankerDeliveryNotFoundException(request.DeleteWaterTankerDeliveryId);
        }

        updateWaterTankerDeliveries.IsDeleted = true;
        _waterTankerDeliveryRepository.Update(updateWaterTankerDeliveries);

        await _mediator.Publish(new WaterTankerDeliveryUpdateEvent(updateWaterTankerDeliveries.Id), cancellationToken);
        return Result.Success(new EntityDeletedResponse(updateWaterTankerDeliveries.Id));
    }
    #endregion
}
