using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Features.Units.Command.GetUnitDetails;
using Dhanman.MyHome.Application.Features.Units.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Command.UnitDetails;

public sealed class GetUnitDetailsCommandHandler : ICommandHandler<GetUnitDetailsCommand, Result<UnitDetailListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Contructor6
    public GetUnitDetailsCommandHandler(  IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    #endregion

    #region Methods
    public async Task<Result<UnitDetailListResponse>> Handle(GetUnitDetailsCommand request, CancellationToken cancellationToken)
    {

        var unitDetailsQuery = _dbContext.SetInt<Domain.Entities.Units.Unit>().AsNoTracking();

        if(request.BuildingIds != null && request.BuildingIds.Count > 0 && !request.BuildingIds.Contains(-1))
        {
            unitDetailsQuery = unitDetailsQuery.Where(e => request.BuildingIds.Contains(e.BuildingId));
        }

        if(request.OccupanyTypeIds != null && request.OccupanyTypeIds.Count > 0 && !request.OccupanyTypeIds.Contains(-1))
        {
            unitDetailsQuery = unitDetailsQuery.Where(e => request.OccupanyTypeIds.Contains(e.OccupancyTypeId));
        }


        var unitDetails = await unitDetailsQuery
        .Select(e => new UnitDetailResponse(
            e.Id,
            e.Name,
            e.CustomerId,
            e.AccountId,
            Convert.ToDecimal(e.Area),
            Convert.ToDecimal(e.BHKType)
        ))
        .ToListAsync(cancellationToken);

        int count = unitDetails.Count;

        await _mediator.Publish(new GetUnitDetailEvent(count));
return Result.Success(new UnitDetailListResponse(unitDetails));
    }
   
    #endregion

}

