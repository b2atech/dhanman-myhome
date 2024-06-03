using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Floors;
using Dhanman.MyHome.Domain.Entities.OccupancyTypes;
using Dhanman.MyHome.Domain.Entities.OccupantTypes;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.UnitTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitsQueryHandler : IQueryHandler<GetAllUnitsQuery, Result<UnitListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllUnitsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<UnitListResponse>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var units = await _dbContext.SetInt<Unit>()
                  .AsNoTracking()
                  .Select(e => new
                  {   Unit = e,
                      NumberOfMembers = _dbContext.SetInt<Resident>()
                                         .Where(r => r.UnitId == e.Id)
                                         .Count()})
                  .Select(e => new UnitResponse(
                          e.Unit.Id,
                          e.Unit.Name,
                          e.Unit.FloorId,
                          _dbContext.SetInt<Floor>()
                              .Where(p => p.Id == e.Unit.FloorId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.Unit.BuildingId,
                          e.Unit.UnitTypeId,
                          _dbContext.SetInt<UnitType>()
                              .Where(p => p.Id == e.Unit.UnitTypeId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.Unit.OccupantTypeId,
                          _dbContext.SetInt<OccupantType>()
                              .Where(p => p.Id == e.Unit.OccupantTypeId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.Unit.OccupancyTypeId,
                          _dbContext.SetInt<OccupancyType>()
                              .Where(p => p.Id == e.Unit.OccupancyTypeId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.NumberOfMembers,
                          e.Unit.Area,
                          e.Unit.BHKType,
                          e.Unit.PhoneExtention,
                          e.Unit.CreatedOnUtc,
                          e.Unit.ModifiedOnUtc,
                          e.Unit.CreatedBy,
                          e.Unit.ModifiedBy))
                  .ToListAsync(cancellationToken);

                  var listResponse = new UnitListResponse(units);

                  return listResponse;
              });
    }
    #endregion

}