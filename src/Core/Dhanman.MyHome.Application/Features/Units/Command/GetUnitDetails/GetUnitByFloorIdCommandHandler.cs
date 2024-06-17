using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Features.Units.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Command.GetUnitDetails;

internal class GetUnitByFloorIdCommandHandler : ICommandHandler<GetUnitByFloorIdCommand, Result<UnitByFloorIdListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Contructor6
    public GetUnitByFloorIdCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    #endregion

    #region Methods
    public async Task<Result<UnitByFloorIdListResponse>> Handle(GetUnitByFloorIdCommand request, CancellationToken cancellationToken)
    {

        var unitDetailsQuery = _dbContext.SetInt<Domain.Entities.Units.Unit>().AsNoTracking();

        if (request.BuildingIds != null && request.BuildingIds.Count > 0 && !request.BuildingIds.Contains(-1))
        {
            unitDetailsQuery = unitDetailsQuery.Where(e => request.BuildingIds.Contains(e.BuildingId));
        }

        if (request.FloorIds != null && request.FloorIds.Count > 0 && !request.FloorIds.Contains(-1))
        {
            unitDetailsQuery = unitDetailsQuery.Where(e => request.FloorIds.Contains(e.FloorId));
        }


        var unitDetails = await unitDetailsQuery
        .Select(e => new UnitByFloorIdResponse(
            e.Id,
            e.Name
        ))
        .ToListAsync(cancellationToken);

        int count = unitDetails.Count;

        await _mediator.Publish(new GetUnitByFloorIdEvent(count));
        return Result.Success(new UnitByFloorIdListResponse(unitDetails));
    }

    #endregion
}
