using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Event;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands;

public sealed class UpdateWaterTankerDeliveryCommandHandler : ICommandHandler<UpdateWaterTankerDeliveryCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IWaterTankerDeliveryRepository _waterTankerDeliveryRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public UpdateWaterTankerDeliveryCommandHandler(IWaterTankerDeliveryRepository waterTankerDeliveryRepository, IMediator mediator)
    {
        _waterTankerDeliveryRepository = waterTankerDeliveryRepository;
        _mediator = mediator;
    }

    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateWaterTankerDeliveryCommand request, CancellationToken cancellationToken)
    {
        var updateWaterTankerDelivery = await _waterTankerDeliveryRepository.GetByIntIdAsync(request.WaterTankerDeliveryId);

        if (updateWaterTankerDelivery == null)
        {
            throw new InvoiceNotFoundException(request.WaterTankerDeliveryId);
        }
        updateWaterTankerDelivery.CompanyId = request.CompanyId;
        updateWaterTankerDelivery.VendorId = request.VendorId;
        updateWaterTankerDelivery.DeliveryDate = request.DeliveryDate;
        updateWaterTankerDelivery.DeliveryTime = request.DeliveryTime;
        updateWaterTankerDelivery.TankerCapacityLiters = request.TankerCapacityLiters;
        updateWaterTankerDelivery.ActualReceivedLiters = request.ActualReceivedLiters;

        _waterTankerDeliveryRepository.Update(updateWaterTankerDelivery);

        await _mediator.Publish(new WaterTankerDeliveryUpdateEvent(updateWaterTankerDelivery.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(updateWaterTankerDelivery.Id));

    }
    #endregion
}
