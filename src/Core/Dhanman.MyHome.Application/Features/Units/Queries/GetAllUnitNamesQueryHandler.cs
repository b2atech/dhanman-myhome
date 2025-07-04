using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitNamesQueryHandler : IQueryHandler<GetAllUnitNamesQuery, Result<UnitNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllUnitNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<UnitNameListResponse>> Handle(GetAllUnitNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var units = await _dbContext.SetInt<Unit>()
                  .AsNoTracking()
                  .Where(e => e.ApartmentId == query.ApartmentId && e.BuildingId == query.BuildingId && e.FloorId == query.FloorId)
                  .OrderBy(e => e.Id)
                  .Select(e => new UnitNameResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new UnitNameListResponse(units);

                  return listResponse;
              });
    }
    #endregion

}
 