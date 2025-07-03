using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;


public class GetAllBuildingNameQueryHandler : IQueryHandler<GetAllBuildingNameQuery, Result<BuildingNameListResponse>>
{
#region Properties
private readonly IApplicationDbContext _dbContext;
#endregion

#region Constructors
public GetAllBuildingNameQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
#endregion

#region Methods
public async Task<Result<BuildingNameListResponse>> Handle(GetAllBuildingNameQuery request, CancellationToken cancellationToken)
{
    return await Result.Success(request)
          .Ensure(query => query != null, Errors.General.EntityNotFound)
          .Bind(async query =>
          {
              var buildings = await _dbContext.SetInt<Building>()
              .AsNoTracking()
              .Where(e => e.ApartmentId == query.ApartmentId)
              .OrderBy(e => e.Id)
              .Select(e => new BuildingNameResponse(
                      e.Id,
                      e.Name))
              .ToListAsync(cancellationToken);

              var listResponse = new BuildingNameListResponse(buildings);

              return listResponse;
          });
}
#endregion

}