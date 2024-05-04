using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitDetailsQueryHandler : IQueryHandler<GetAllUnitDetailsQuery, Result<UnitDetailListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllUnitDetailsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<UnitDetailListResponse>> Handle(GetAllUnitDetailsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
        .Ensure(query => query.BuildingId != null, Errors.General.EntityNotFound)
        .Bind(async query =>
        {
            var unitDetails = await _dbContext.SetInt<Unit>()
            .AsNoTracking()
            .Where(e => e.BuildingId == query.BuildingId && e.OccupancyTypeId == query.OccupancyTypeId)
            .Select(e => new UnitDetailResponse(
                          e.Id,
                          e.Name,   
                          e.CustomerId,
                          e.FloorId,
                          _dbContext.SetInt<Floor>()
                              .Where(p => p.Id == e.FloorId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.UnitTypeId,
                          _dbContext.SetInt<UnitType>()
                              .Where(p => p.Id == e.UnitTypeId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.OccupantTypeId,
                          _dbContext.SetInt<OccupantType>()
                              .Where(p => p.Id == e.OccupantTypeId)
                              .Select(p => p.Name).FirstOrDefault(),                                            
                          e.Area,
                          e.BHKType,
                          e.AccountId))
                    .ToListAsync(cancellationToken);

            var listResponse = new UnitDetailListResponse(unitDetails);
            return listResponse;
        });

    }
    #endregion
}