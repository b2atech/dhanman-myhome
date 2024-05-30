using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Floors;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetFloorByFloorIdQueryHandler : IQueryHandler<GetFloorByFloorIdQuery, Result<FloorListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetFloorByFloorIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<FloorListResponse>> Handle(GetFloorByFloorIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var floors = await _dbContext.SetInt<Floor>()
                  .AsNoTracking()
                  .Where(f => f.Id == request.FloorId)
                  .Select(f => new FloorResponse(
                          f.Id,
                          f.Name,
                          f.ApartmentId,
                          f.BuildingId,
                          _dbContext.SetInt<Building>()
                          .Where(building => building.Id == f.BuildingId)
                          .Select(building => building.Name)
                          .FirstOrDefault(),
                          f.TotalUnits,
                          f.CreatedBy,
                          f.CreatedOnUtc,
                          f.ModifiedBy,
                          f.ModifiedOnUtc))
                  .ToListAsync(cancellationToken);

                  var listResponse = new FloorListResponse(floors);

                  return listResponse;
              });
    }
    #endregion
}
