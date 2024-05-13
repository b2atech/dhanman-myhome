using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;
using Dhanman.MyHome.Domain.Entities.ServiceProviderSubTypes;

namespace Dhanman.MyHome.Application.Features.ServiceProviderSubType.Queries;

internal class GetAllServiceProviderSubTypeQueryHandler : IQueryHandler<GetAllServiceProviderSubTypeQuery, Result<ServiceProivderSubTypeListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllServiceProviderSubTypeQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ServiceProivderSubTypeListResponse>> Handle(GetAllServiceProviderSubTypeQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var spSubType = await _dbContext.SetInt<ServiveProviderSubType>()
                  .AsNoTracking()
                  .Select(e => new ServiceProivderSubTypeResponse(
                          e.Id,
                          e.ServiceProviderTypeId,
                          e.Name
                          ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ServiceProivderSubTypeListResponse(spSubType);

                  return listResponse;
              });
    }
    #endregion
}
