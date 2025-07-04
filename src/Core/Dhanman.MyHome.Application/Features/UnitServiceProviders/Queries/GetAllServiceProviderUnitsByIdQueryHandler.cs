using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.UnitServiceProviders;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.UnitServiceProviders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Queries;

public class GetAllServiceProviderUnitsByIdQueryHandler : IQueryHandler<GetAllServiceProviderUnitsByIdQuery, Result<AssignUnitListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetAllServiceProviderUnitsByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<AssignUnitListResponse>> Handle(GetAllServiceProviderUnitsByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var unit = await _dbContext.SetInt<UnitServiceProvider>()
                  .AsNoTracking()
                  .Where(spu => spu.ServiceProviderId == request.ServiceProviderId)
                  .Select(spu => new ServiceProviderAssignedUnitResponse(
                        spu.Id,
                        spu.UnitId,
                        _dbContext.SetInt<Unit>()
                              .Where(u => u.Id == spu.UnitId)
                              .Select(u => u.Name).FirstOrDefault()
                        ))
                  .ToListAsync(cancellationToken);
                var response = new AssignUnitListResponse(unit);
                return response;
            });
    }
    #endregion
}
