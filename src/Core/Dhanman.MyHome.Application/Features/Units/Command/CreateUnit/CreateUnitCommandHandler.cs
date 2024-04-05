using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;
using System.Text.RegularExpressions;
using Unit = Dhanman.MyHome.Domain.Entities.Apartments.Unit;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateUnit;

public class CreateUnitCommandHandler : ICommandHandler<CreateUnitCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IUnitRepository _unitRepository;
    private readonly IUnitTypeRepository _unitTypeRepository;
    private readonly IOccupancyTypeRepository _occupancyTypeRepository;
    private readonly IOccupantTypeRepository _occupantTypeRepository;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateUnitCommandHandler(IUnitRepository unitRepository, IUnitTypeRepository unitTypeRepository, IOccupantTypeRepository occupantTypeRepository, IOccupancyTypeRepository occupancyTypeRepository, IMediator mediator, IApplicationDbContext applicationDbContext)
    {
        _unitRepository = unitRepository;
        _unitTypeRepository = unitTypeRepository;
        _occupancyTypeRepository = occupancyTypeRepository;
        _occupantTypeRepository = occupantTypeRepository;
        _mediator = mediator;
        _dbContext = applicationDbContext;
    }
    #endregion

    #region Methodes
    
    public async Task<Result<EntityCreatedResponse>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var unitList = new List<Unit>();
        int nextunitId = _unitRepository.GetTotalRecordsCount() + 1;
        foreach (var item in request.UnitList)
        {
            
            int floorId = GetFloorId(item.Flat);

            int unitTypeId = await _unitTypeRepository.GetBydNameAsync(item.FlatType);
            int occupancyTypeId = await _occupancyTypeRepository.GetBydNameAsync(item.Occupancy);
            int occupantTypeId = await _occupantTypeRepository.GetBydNameAsync(item.Occupant);
            var unit = new Unit(nextunitId, item.Flat, floorId, unitTypeId, occupantTypeId, occupancyTypeId, item.PrimaryEIntercom, item.EIntercom,  item.Latitude, item.Longitude, item.CreatedBy);

            unitList.Add(unit);
            nextunitId++;

        }

        _dbContext.SetInt<Unit>().AddRange(unitList);

        var unitIds = unitList.Select(u => u.Id).ToList();
        await _mediator.Publish(new UnitCreatedEvent(unitIds), cancellationToken);

        return Result.Success(new EntityCreatedResponse(unitIds));
    }

    private int GetFloorId(string flat)
    {
        string pattern = @"\d";
        string input = flat;
        TimeSpan timeout = TimeSpan.FromSeconds(1);
        Match match = Regex.Match(input, pattern, RegexOptions.None, timeout);
        return int.Parse(match.Value);
    }

    #endregion
}
