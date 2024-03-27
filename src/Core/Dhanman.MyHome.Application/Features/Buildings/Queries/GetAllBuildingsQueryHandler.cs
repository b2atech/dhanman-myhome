using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;

public class GetAllBuildingsQueryHandler : IQueryHandler<GetAllBuildingsQuery, Result<BuildingListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllBuildingsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<BuildingListResponse>> Handle(GetAllBuildingsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var buildings = await _dbContext.SetInt<Building>()
                  .AsNoTracking()                  
                  .Select(e => new BuildingResponse(
                          e.Id,
                          e.Name,
                          e.BuildingTypeId,
                          _dbContext.SetInt<BuildingType>()
                              .Where(p => p.Id == e.BuildingTypeId)
                              .Select(p => p.Name).FirstOrDefault(),                          
                          e.TotalUnits,
                          e.CreatedOnUtc,
                          e.ModifiedOnUtc,
                          e.CreatedBy,
                          e.ModifiedBy))
                  .ToListAsync(cancellationToken);

                  var listResponse = new BuildingListResponse(buildings);

                  return listResponse;
              });
    }
    #endregion

}