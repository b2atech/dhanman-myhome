using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Floors;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Floors;
using Dhanman.MyHome.Domain;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetAllFloorsQueryHandler : IQueryHandler<GetAllFloorsQuery, Result<FloorListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllFloorsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<FloorListResponse>> Handle(GetAllFloorsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var floors = await _dbContext.SetInt<Floor>()
                  .AsNoTracking()
                  .Where(e => e.ApartmentId == request.ApartmentId && e.BuildingId == request.BuildingId)
                  .Select(e => new FloorResponse(
                          e.Id,
                          e.Name,
                          e.ApartmentId,
                          e.BuildingId,
                          _dbContext.SetInt<Building>()
                          .Where(building => building.Id == e.BuildingId)
                          .Select(building => building.Name)
                          .FirstOrDefault(),
                          e.TotalUnits,
                          e.CreatedBy,
                          e.CreatedOnUtc,
                          e.ModifiedBy,
                          e.ModifiedOnUtc))
                  .ToListAsync(cancellationToken);

                  var listResponse = new FloorListResponse(floors);

                  return listResponse;
              });
    }
    #endregion

}