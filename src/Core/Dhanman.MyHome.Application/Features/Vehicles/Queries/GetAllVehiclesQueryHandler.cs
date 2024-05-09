using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.Vehicals;
using Dhanman.MyHome.Domain.Entities.VehicleTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Vehicles.Queries;

public class GetAllVehiclesQueryHandler : IQueryHandler<GetAllVehiclesQuery, Result<VehicleListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllVehiclesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VehicleListResponse>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var vehicles = await _dbContext.SetInt<Vehicle>()
                  .AsNoTracking()
                  .Select(e => new VehicleResponse(
                          e.Id,
                          e.VehicleNumber,
                          e.VehicleTypeId,
                          _dbContext.SetInt<VehicleType>()
                                  .Where(p => p.Id == e.VehicleTypeId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.UnitId,
                          _dbContext.SetInt<Unit>()
                                  .Where(p => p.Id == e.UnitId)
                                  .Select(p => p.Name).FirstOrDefault(),                         
                          e.CreatedOnUtc,
                          e.ModifiedOnUtc,
                          e.CreatedBy,
                          e.ModifiedBy))
                  .ToListAsync(cancellationToken);

                  var listResponse = new VehicleListResponse(vehicles);

                  return listResponse;
              });
    }
    #endregion

}