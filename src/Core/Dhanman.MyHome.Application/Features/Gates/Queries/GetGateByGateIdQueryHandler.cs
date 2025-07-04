using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Gates;
using Dhanman.MyHome.Domain.Entities.GateTypes;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Gates.Queries;

public class GetGateByGateIdQueryHandler : IQueryHandler<GetGateByGateIdQuery, Result<GateResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetGateByGateIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<GateResponse>> Handle(GetGateByGateIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var gateResponse = await (from gate in _dbContext.SetInt<Gate>().AsNoTracking()
                                            where gate.Id == request.GateId
                                            join apartment in _dbContext.Set<Apartment>()
                                            on gate.ApartmentId equals apartment.Id
                                            join building in _dbContext.SetInt<Building>()
                                            on gate.BuildingId equals building.Id
                                            join gateType in _dbContext.SetInt<GateType>()
                                            on gate.GateTypeId equals gateType.Id
                                            join createdByUser in _dbContext.Set<User>()
                                        on building.CreatedBy
                                        equals createdByUser.Id into createdByUserGroup
                                            from createdByUser in createdByUserGroup.DefaultIfEmpty() // Left join for CreatedBy user
                                            join modifiedByUser in _dbContext.Set<User>()
                                                on building.ModifiedBy
                                                equals modifiedByUser.Id into modifiedByUserGroup
                                            from modifiedByUser in modifiedByUserGroup.DefaultIfEmpty() // Left join for ModifiedBy user
                                            select new GateResponse(
                                                                 gate.Id,
                                                                 gate.Name,
                                                                 gate.ApartmentId,
                                                                 apartment.Name,
                                                                 gate.BuildingId,
                                                                 building.Name,
                                                                 gate.GateTypeId,
                                                                 gateType.Name,
                                                                 gate.IsUsedForIn,
                                                                 gate.IsUsedForOut,
                                                                 gate.IsAllUsersAllowed,
                                                                 gate.IsResidentsAllowed,
                                                                 gate.IsStaffAllowed,
                                                                 gate.IsVendorAllowed,
                                                                 gate.CreatedBy,
                                                                 gate.ModifiedBy,
                                                                 gate.CreatedOnUtc,
                                                                 gate.ModifiedOnUtc,
                                                           $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                                           $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}"
                                                           ))
                                          .FirstOrDefaultAsync(cancellationToken);

                  return gateResponse != null
                      ? Result.Success(gateResponse)
                      : Result.Failure<GateResponse>(Errors.General.EntityNotFound);
              });
    }
    #endregion

}