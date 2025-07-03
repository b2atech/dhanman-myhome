using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Abstractions;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Models;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
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
        private readonly ICommonServiceClient _commonServiceClient;
        private readonly ISalesServiceClient _salesServiceClient;
        private readonly IUserContextService _userContextService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Constructor
        public CreateUnitCommandHandler(IUnitRepository unitRepository, IMediator mediator, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IUserContextService userContextService, IUnitOfWork unitOfWork,IEventPublisher eventPublisher
    )
        {
            _unitRepository = unitRepository;
            _mediator = mediator;
            _commonServiceClient = commonServiceClient;
            _salesServiceClient = salesServiceClient; 
            _userContextService = userContextService;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
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

            MessageContext messageContext = new MessageContext
            {
                UserId = _userContextService.CurrentUserId,
                CorrelationId = _userContextService.CorrelationId,
                OrganizationId = _userContextService.OrganizationId,

            };
            var command = new CreateBasicCustomerCommand(unit.CustomerId, unit.ApartmentId, messageContext.OrganizationId, request.Name,null, null, null, null, null, null, null, 0, false, 0, false, messageContext);

            var eventEnevlop = new EventEnvelope<CreateBasicCustomerCommand>()
            {
                EventType = EventTypes.CommunityCustomerAfterUnitCreated,
                Source = "CommunityService",
                UserId = messageContext.UserId,
                CorrelationId = messageContext.CorrelationId,
                OrganizationId = messageContext.OrganizationId,
                Payload = command,
                // CommandType = RoutingKeys.Community.UnitAsCustomerCreated,
            };
            await _eventPublisher.PublishAsync(eventEnevlop);

          //  await _commonServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = request.Name, CompanyId = request.ApartmentId, CreatedBy = _userContextService.CurrentUserId });
          //  await _salesServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = request.Name, CompanyId = request.ApartmentId, CreatedBy = _userContextService.CurrentUserId });

            await _mediator.Publish(new UnitCreatedEvent(unit.Id), cancellationToken);

            return Result.Success(new EntityCreatedResponse(unit.Id));
        }
        #endregion
    }
}
