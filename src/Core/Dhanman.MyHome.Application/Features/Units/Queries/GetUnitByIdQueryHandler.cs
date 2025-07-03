using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.Users;
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
                var unitResponse = await (from unit in _dbContext.SetInt<Unit>().AsNoTracking()
                  where unit.Id == request.UnitId
                join createdByUser in _dbContext.Set<User>()
                       on unit.CreatedBy equals createdByUser.Id into createdByUserGroup
                from createdByUser in createdByUserGroup.DefaultIfEmpty() // Left join for CreatedBy user
                join modifiedByUser in _dbContext.Set<User>()
                       on unit.ModifiedBy equals modifiedByUser.Id into modifiedByUserGroup
                from modifiedByUser in modifiedByUserGroup.DefaultIfEmpty()
                                          join residentUnit in _dbContext.SetInt<ResidentUnit>() // Join resident_units table
                                                                        on unit.Id equals residentUnit.UnitId
                                          join resident in _dbContext.SetInt<Resident>() // Join residents table
                                              on residentUnit.ResidentId equals resident.Id
                                          join user in _dbContext.Set<User>() // Join users to get customer details
                                              on resident.UserId equals user.Id
                                          where residentUnit.IsDeleted == false// Left join for ModifiedBy user
                                          select new UnitResponse(
                                                unit.Id,
                                                unit.Name,
                                                unit.FloorId,
                                                unit.BuildingId,
                                                unit.UnitTypeId,
                                                unit.CustomerId,
                                                unit.OccupantTypeId,
                                                unit.OccupancyTypeId,
                                                unit.Area,
                                                unit.BHKType,
                                                unit.PhoneExtention,
                                                unit.EIntercom,
                                                unit.CreatedOnUtc,
                                                unit.ModifiedOnUtc,
                                                unit.CreatedBy,
                                                unit.ModifiedBy,
                                                $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                                $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}",
                                                $"{user.FirstName.Value} {user.LastName.Value}",
                                                user.ContactNumber
                                          ))
                                          .FirstOrDefaultAsync(cancellationToken);
                                        return unitResponse != null
                                        ? Result.Success(unitResponse)
                                        : Result.Failure<UnitResponse>(Errors.General.EntityNotFound);
            });
    }
    #endregion

}
