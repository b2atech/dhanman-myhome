using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Abstractions;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Models;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.Shared.Contracts.Commands;
using Dhanman.Shared.Contracts.Events;
using Dhanman.Shared.Contracts.Routing;
using MediatR;
using Unit = Dhanman.MyHome.Domain.Entities.Units.Unit;


namespace Dhanman.MyHome.Application.Features.Units.Command.CreateUnits
{
    public class CreateUnitCommandHandler : ICommandHandler<CreateUnitCommand, Result<EntityCreatedResponse>>
    {
        #region Properties
        private readonly IUnitRepository _unitRepository;
        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandPublisher _commandPublisher;

        #endregion

        #region Constructor
        public CreateUnitCommandHandler(IUnitRepository unitRepository, IMediator mediator, IUserContextService userContextService, IUnitOfWork unitOfWork, ICommandPublisher commandPublisher
    )
        {
            _unitRepository = unitRepository;
            _mediator = mediator;
            _userContextService = userContextService;
            _unitOfWork = unitOfWork;
            _commandPublisher = commandPublisher;
        }
        #endregion

        #region Methods
        public async Task<Result<EntityCreatedResponse>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            //int lastId = await _unitRepository.GetLastUnitIdAsync();
            //int newId = lastId + 1;

            var unit = new Unit(
                   request.Name, 
                   request.BuildingId,
                   request.FloorId,
                   request.UnitTypeId,
                   request.OccupantId,
                   request.OccupancyId,
                   request.Area,
                   request.Bhk,
                   request.PhoneExtension,
                   request.EIntercom,
                    "1.0",
                    "1.1",
                   request.ApartmentId,
                   Guid.NewGuid()
                    );

            _unitRepository.Insert(unit);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            MessageContext messageContext = new ()
            {
                UserId = _userContextService.CurrentUserId,
                CorrelationId = _userContextService.CorrelationId,
                OrganizationId = _userContextService.OrganizationId,
            };
            var command = new CreateBasicCustomerCommand(unit.CustomerId, unit.ApartmentId, messageContext.OrganizationId, request.Name,null, null, null, null, null, null, null, 0, false, 0, false, messageContext);

            var commandEnevlopCommon = new CommandEnvelope<CreateBasicCustomerCommand>()
            {
                CommandType = RoutingKeys.Community.CreateCustomerInSalesAfterUnit,
                Source = "CommunityService",
                UserId = messageContext.UserId,
                CorrelationId = messageContext.CorrelationId,
                OrganizationId = messageContext.OrganizationId,
                Payload = command,
            };
            await _commandPublisher.PublishAsync(RoutingKeys.Community.CreateCustomerInSalesAfterUnit, commandEnevlopCommon);

            await _mediator.Publish(new UnitCreatedEvent(unit.Id), cancellationToken);

            return Result.Success(new EntityCreatedResponse(unit.Id));
        }
        #endregion
    }
}
