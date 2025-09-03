using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Vehicals;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Vehicles.Queries;

public class GetVehiclesByUnitIdQueryHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetVehiclesByUnitIdQuery, Result<BasicVehicleInfoListResponse>>
{

    #region Methods
    public async Task<Result<BasicVehicleInfoListResponse>> Handle(GetVehiclesByUnitIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var vehicles = await _dbContext.SetInt<VehicleInfo>()
                    .FromSqlInterpolated($"SELECT * FROM public.get_vehicles_by_unit({request.UnitId})")
                    .AsNoTracking()
                    .Select(v => new BasicVehicleInfoResponse(
                        v.Id,
                        v.VehicleNumber,
                        v.VehicleTypeId,
                        v.UnitId,
                        v.VehicleRfId,
                        v.VehicleRfIdSecretcode,
                        v.CreatedOnUtc
                    ))
                    .ToListAsync(cancellationToken);

                var listResponse = new BasicVehicleInfoListResponse(vehicles);

                return listResponse;
            });
    }

    #endregion
}