using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.BuildingTypes;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;

public class GetBuildingByIdQueryHandler : IQueryHandler<GetBuildingByIdQuery, Result<BuildingResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetBuildingByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<BuildingResponse>> Handle(GetBuildingByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var buildingResponse = await (from building in _dbContext.SetInt<Building>().AsNoTracking()
                                         where building.Id == request.BuildingId
                                         join buildingType in _dbContext.SetInt<BuildingType>().AsNoTracking()
                                            on building.BuildingTypeId equals buildingType.Id
                                         join createdByUser in _dbContext.Set<User>()
                                             on building.CreatedBy
                                             equals createdByUser.Id into createdByUserGroup
                                             from createdByUser in createdByUserGroup.DefaultIfEmpty() // Left join for CreatedBy user
                                         join modifiedByUser in _dbContext.Set<User>()
                                             on building.ModifiedBy
                                             equals modifiedByUser.Id into modifiedByUserGroup
                                         from modifiedByUser in modifiedByUserGroup.DefaultIfEmpty() // Left join for ModifiedBy user

                                                select new BuildingResponse(
                                                  building.Id,
                                                  building.Name,
                                                  building.BuildingTypeId,
                                                  buildingType.Name,
                                                  building.TotalUnits,
                                                  building.CreatedOnUtc,
                                                  building.ModifiedOnUtc,
                                                  building.CreatedBy,
                                                  building.ModifiedBy,
                                                  $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                                  $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}"
                                                  ))
                                         .FirstOrDefaultAsync(cancellationToken);

                  return buildingResponse != null
                     ? Result.Success(buildingResponse)
                     : Result.Failure<BuildingResponse>(Errors.General.EntityNotFound);

              });
    }
    #endregion

}