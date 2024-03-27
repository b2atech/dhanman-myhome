using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;

public class GetAllBuildingNamesQueryHandler : IQueryHandler<GetAllBuildingNamesQuery, Result<BuildingNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllBuildingNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<BuildingNameListResponse>> Handle(GetAllBuildingNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var buildings = await _dbContext.SetInt<Building>()
                  .AsNoTracking()
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