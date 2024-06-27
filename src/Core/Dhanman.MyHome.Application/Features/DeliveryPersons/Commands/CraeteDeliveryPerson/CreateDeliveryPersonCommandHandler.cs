using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.DeliveryPersons.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Deliveries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.DeliveryPersons.Commands.CreateDeliveryPerson;

public class CreateDeliveryPersonCommandHandler : ICommandHandler<CreateDeliveryPersonCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IDeliveryPersonRepository _deliveryPersonRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateDeliveryPersonCommandHandler(IDeliveryPersonRepository deliveryPersonRepository, IMediator mediator)
    {
        _deliveryPersonRepository = deliveryPersonRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateDeliveryPersonCommand request, CancellationToken cancellationToken)
    {
        var deliveryPersonRequest = new DeliveryPerson(request.Name, request.CompanyName, request.MobileNumber);
        _deliveryPersonRepository.Insert(deliveryPersonRequest);

        await _mediator.Publish(new DeliveryPersonCreatedEvent(deliveryPersonRequest.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(deliveryPersonRequest.Id));
    }
    #endregion
}

