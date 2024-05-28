using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.BuildingTypes;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.BuildingTypes;

namespace Dhanman.MyHome.Application.Features.BuildingTypes.Queries;

public class GetAllBuildingTypesQueryHandler : IQueryHandler<GetAllBuildingTypesQuery, Result<BuildingTypeListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllBuildingTypesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion
    public async Task<Result<BuildingTypeListResponse>> Handle(GetAllBuildingTypesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
             .Ensure(query => query != null, Errors.General.EntityNotFound)
             .Bind(async query =>
             {
                 var buildingTypes = await _dbContext.SetInt<BuildingType>()
                 .AsNoTracking()
                 .Select(e => new BuildingTypeResponse(
                     e.Id,
                     e.Name))
                 .ToListAsync(cancellationToken);

                 var listResponse = new BuildingTypeListResponse(buildingTypes);
                 return listResponse;
             }
            );
    }
}
