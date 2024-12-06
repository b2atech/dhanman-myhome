using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
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
        #endregion

        #region Constructor
        public CreateUnitCommandHandler(IUnitRepository unitRepository, IMediator mediator, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IUserContextService userContextService)
        {
            _unitRepository = unitRepository;
            _mediator = mediator;
            _commonServiceClient = commonServiceClient;
            _salesServiceClient = salesServiceClient; 
            _userContextService = userContextService;
        }
        #endregion

        #region Methods
        public async Task<Result<EntityCreatedResponse>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            int lastId = await _unitRepository.GetLastUnitIdAsync();
            int newId = lastId + 1;

            var unit = new Unit(newId, 
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

            _unitRepository.Insert( unit );

            await _commonServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = request.Name, CompanyId = request.ApartmentId, CreatedBy = _userContextService.GetCurrentUserId() });
            await _salesServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = request.Name, CompanyId = request.ApartmentId, CreatedBy = _userContextService.GetCurrentUserId() });

            await _mediator.Publish(new UnitCreatedEvent(unit.Id), cancellationToken);

            return Result.Success(new EntityCreatedResponse(unit.Id));
        }
        #endregion
    }
}
