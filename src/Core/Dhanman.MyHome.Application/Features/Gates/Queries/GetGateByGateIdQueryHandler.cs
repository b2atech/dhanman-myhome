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

public class GetGateByGateIdQueryHandler : IQueryHandler<GetGateByGateIdQuery, Result<GateListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetGateByGateIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<GateListResponse>> Handle(GetGateByGateIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var gates = await _dbContext.SetInt<Gate>()
                  .AsNoTracking()
                   .Where(g => g.Id == request.GateId)
                  .Select(g => new GateResponse(
                          g.Id,
                          g.Name,
                          g.ApartmentId,
                          _dbContext.Set<Apartment>()
                          .Where(apartment => apartment.Id == g.ApartmentId)
                          .Select(apartment => apartment.Name)
                          .FirstOrDefault(),
                          g.BuildingId,
                          _dbContext.SetInt<Building>()
                          .Where(building => building.Id == g.BuildingId)
                          .Select(Building => Building.Name)
                          .FirstOrDefault(),
                          g.GateTypeId,
                          _dbContext.SetInt<GateType>()
                          .Where(gateType => gateType.Id == g.GateTypeId)
                          .Select(gateType => gateType.Name)
                          .FirstOrDefault(),
                          g.IsUsedForIn,
                          g.IsUsedForOut,
                          g.IsAllUsersAllowed,
                          g.IsResidentsAllowed,
                          g.IsStaffAllowed,
                          g.IsVendorAllowed,
                          g.CreatedBy,
                          g.CreatedOnUtc))
                  .ToListAsync(cancellationToken);

                  var listResponse = new GateListResponse(gates);

                  return listResponse;
              });
    }
    #endregion

}