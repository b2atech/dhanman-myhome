using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Vehicles.Queries;

public class GetAllVehicleNamesQueryHandler : IQueryHandler<GetAllVehicleNamesQuery, Result<VehicleNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllVehicleNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VehicleNameListResponse>> Handle(GetAllVehicleNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var vehicles = await _dbContext.SetInt<Vehicle>()
                  .AsNoTracking()
                  .Select(e => new VehicleNameResponse(
                          e.Id,
                          e.VehicleNumber))
                  .ToListAsync(cancellationToken);

                  var listResponse = new VehicleNameListResponse(vehicles);

                  return listResponse;
              });
    }
    #endregion

}