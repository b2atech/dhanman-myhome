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
                  var floors = await (from floor in _dbContext.SetInt<Floor>().AsNoTracking()
                                      where floor.ApartmentId == request.ApartmentId
                                      join building in _dbContext.SetInt<Building>().AsNoTracking()
                                      on floor.BuildingId equals building.Id
                                      select new FloorResponse(
                                          floor.Id,
                                          floor.Name,
                                          floor.ApartmentId,
                                          floor.BuildingId,
                                          building.Name,
                                          floor.TotalUnits,
                                          floor.CreatedBy,
                                          floor.CreatedOnUtc,
                                          floor.ModifiedBy,
                                          floor.ModifiedOnUtc))
                      .ToListAsync(cancellationToken);

                  var listResponse = new FloorListResponse(floors);

                  return listResponse;
              });
    }
    #endregion

}