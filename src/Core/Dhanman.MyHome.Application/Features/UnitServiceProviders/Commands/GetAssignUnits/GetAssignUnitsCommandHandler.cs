using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.UnitServiceProviders;
using Dhanman.MyHome.Application.Features.Units.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.GetAssignUnits;

public sealed class GetAssignUnitsCommandHandler : ICommandHandler<GetAssignUnitsCommand, Result<AssignSPUnitsListResponse>>

{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Contructor6
    public GetAssignUnitsCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    #endregion

    #region Methods
    public async Task<Result<AssignSPUnitsListResponse>> Handle(GetAssignUnitsCommand request, CancellationToken cancellationToken)
    {

        var unitDetailsQuery = _dbContext.SetInt<Domain.Entities.Units.Unit>().AsNoTracking();

        if (request.BuildingIds != null && request.BuildingIds.Count > 0 && !request.BuildingIds.Contains(-1))
        {
            unitDetailsQuery = unitDetailsQuery.Where(e => request.BuildingIds.Contains(e.BuildingId));
        }
        var unitDetails = await unitDetailsQuery
        .Select(e => new AssignSPUnitsResponse(
            e.Id,
            e.Name,
            e.CustomerId
        ))
        .ToListAsync(cancellationToken);

        int count = unitDetails.Count;

        await _mediator.Publish(new GetUnitDetailEvent(count));
        return Result.Success(new AssignSPUnitsListResponse(unitDetails));
    }

    #endregion

}
