using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Gates;
using Dhanman.MyHome.Domain.Entities.GateTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Gates.Queries;

public class GetAllGatesQueryHandler : IQueryHandler<GetAllGatesQuery, Result<GateListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllGatesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<GateListResponse>> Handle(GetAllGatesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {

                  var gates = await (from gate in _dbContext.SetInt<Gate>().AsNoTracking()
                                      where gate.ApartmentId == request.ApartmentId
                                      join apartment in _dbContext.Set<Apartment>()
                                      on gate.ApartmentId equals apartment.Id
                                      join building in _dbContext.SetInt<Building>()
                                      on gate.BuildingId equals building.Id
                                      join gateType in _dbContext.SetInt<GateType>()
                                      on gate.GateTypeId equals gateType.Id
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
                                                           gate.CreatedOnUtc))
                                .ToListAsync(cancellationToken);
                 
                  var listResponse = new GateListResponse(gates);

                  return listResponse;
              });
    }
    #endregion

}