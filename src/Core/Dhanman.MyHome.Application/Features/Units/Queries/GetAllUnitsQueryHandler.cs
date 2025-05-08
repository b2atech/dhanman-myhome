using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Companies;
using Dhanman.MyHome.Domain.Entities.Floors;
using Dhanman.MyHome.Domain.Entities.OccupancyTypes;
using Dhanman.MyHome.Domain.Entities.OccupantTypes;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.UnitTypes;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitsQueryHandler : IQueryHandler<GetAllUnitsQuery, Result<UnitListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllUnitsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<UnitListResponse>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var company = await _dbContext.Set<Company>().FirstOrDefaultAsync(e => e.Id == request.ApartmentId);

                  var companyIds = request.IsGetForAllOrganization ?
                                   await _dbContext.Set<Company>()
                                   .Where(e => e.OrganizationId == company.OrganizationId)
                                   .Select(e => e.Id).ToListAsync(cancellationToken)
                                   : new List<Guid>() { request.ApartmentId };

                  var units = await (from unit in _dbContext.SetInt<Unit>().AsNoTracking()
                                     where companyIds.Contains(unit.ApartmentId)
                                     join floor in _dbContext.SetInt<Floor>()
                                     on unit.FloorId equals floor.Id
                                     join unitType in _dbContext.SetInt<UnitType>()
                                     on unit.UnitTypeId equals unitType.Id
                                     join occupantType in _dbContext.SetInt<OccupantType>()
                                     on unit.OccupantTypeId equals occupantType.Id
                                     join occupancyType in _dbContext.SetInt<OccupancyType>()
                                     on unit.OccupancyTypeId equals occupancyType.Id
                                     let numberOfMembers = _dbContext.SetInt<ResidentUnit>()
                                         .Where(ru => ru.UnitId == unit.Id)
                                         .Count()
                                     join createdByUser in _dbContext.Set<User>()
                                     on unit.CreatedBy
                                     equals createdByUser.Id into createdByUserGroup
                                     from createdByUser in createdByUserGroup.DefaultIfEmpty() // Left join for CreatedBy user
                                     join modifiedByUser in _dbContext.Set<User>()
                                         on unit.ModifiedBy
                                         equals modifiedByUser.Id into modifiedByUserGroup
                                     from modifiedByUser in modifiedByUserGroup.DefaultIfEmpty() // Left join for ModifiedBy user
                                     select new UnitResponse(
                                         unit.Id,
                                         unit.Name,
                                         unit.FloorId,
                                         floor.Name,
                                         unit.BuildingId,
                                         unit.UnitTypeId,
                                         unitType.Name,
                                         unit.CustomerId,
                                         unit.OccupantTypeId,
                                         occupantType.Name,
                                         unit.OccupancyTypeId,
                                         occupancyType.Name,
                                         numberOfMembers,
                                         unit.Area,
                                         unit.BHKType,
                                         unit.PhoneExtention,
                                         unit.CreatedOnUtc,
                                         unit.ModifiedOnUtc,
                                         unit.CreatedBy,
                                         unit.ModifiedBy,
                                         $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                         $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}"
                                         ))
                                    .ToListAsync(cancellationToken);

                  var listResponse = new UnitListResponse(units);

                  return listResponse;
              });
    }
    #endregion

}