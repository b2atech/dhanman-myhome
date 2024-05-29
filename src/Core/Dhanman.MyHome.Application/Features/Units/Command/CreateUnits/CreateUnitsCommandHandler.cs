using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;
using Unit = Dhanman.MyHome.Domain.Entities.Units.Unit;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateUnits
{
    public class CreateUnitsCommandHandler : ICommandHandler<CreateUnitsCommand, Result<EntityCreatedResponse>>
    {
        #region Properties
        private readonly IUnitRepository _unitRepository;
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _dbContext;
        #endregion

        #region Constructor
        public CreateUnitsCommandHandler(IUnitRepository unitRepository, IMediator mediator, IApplicationDbContext dbContext)
        {
            _unitRepository = unitRepository;
            _mediator = mediator;
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<Result<EntityCreatedResponse>> Handle(CreateUnitsCommand request, CancellationToken cancellationToken)
        {
            var unitList = new List<Unit>();
            int nextunitId = _unitRepository.GetTotalRecordsCount() + 1;
            foreach (var item in request.UnitsList)
            {
                var unit = new Unit(nextunitId,item.Name, item.BuildingId,
                    item.FloorId,
                    item.UnitTypeId,
                    item.OccupantId,
                    item.OccupancyId,
                    item.Area,
                    item.Bhk,
                    item.PhoneExtension,
                    item.EIntercom,
                    "1.0",
                    "1.1",
                    Guid.NewGuid()
                    );
                unitList.Add(unit);
                nextunitId++;

            }
            _dbContext.SetInt<Unit>().AddRange(unitList);
            var unitIds = unitList.Select(u => u.Id).ToList();
            await _mediator.Publish(new UnitCreatedEvent(unitIds), cancellationToken);

            return Result.Success(new EntityCreatedResponse(unitIds));
        }
        #endregion
    }
}
