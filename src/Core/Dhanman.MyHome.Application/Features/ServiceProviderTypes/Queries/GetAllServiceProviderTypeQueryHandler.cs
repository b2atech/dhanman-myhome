using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ServiceProviderTypes;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.ServiceProviderTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.ServiceProviderTypes.Queries;

internal class GetAllServiceProviderTypeQueryHandler : IQueryHandler<GetAllServiceProviderTypeQuery, Result<ServiceProivderTypeListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllServiceProviderTypeQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ServiceProivderTypeListResponse>> Handle(GetAllServiceProviderTypeQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var spType = await _dbContext.SetInt<ServiceProviderType>()
                  .AsNoTracking()
                  .Select(e => new ServiceProivderTypeResponse(
                          e.Id,
                          e.Name
                          ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ServiceProivderTypeListResponse(spType);

                  return listResponse;
              });
    }
    #endregion
}
