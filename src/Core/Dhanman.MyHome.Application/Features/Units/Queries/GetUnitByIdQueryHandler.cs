using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitByIdQueryHandler : IQueryHandler<GetUnitByIdQuery, Result<UnitResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetUnitByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<UnitResponse>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var unit = await _dbContext.SetInt<Unit>()
                  .AsNoTracking()
                  .Where(u => u.Id == request.UnitId)
                  .Select(u => new UnitResponse(
                        u.Id,
                        u.Name,
                        u.FloorId,
                        u.BuildingId,
                        u.UnitTypeId,
                        u.CustomerId,
                        u.OccupantTypeId,
                        u.OccupancyTypeId,
                        u.Area,
                        u.BHKType,
                        u.PhoneExtention,
                        u.EIntercom,
                        u.CreatedOnUtc,
                        u.ModifiedOnUtc,
                        u.CreatedBy,
                  u.ModifiedBy))
                  .FirstOrDefaultAsync(cancellationToken);
                return unit;
            });
    }
    #endregion

}
