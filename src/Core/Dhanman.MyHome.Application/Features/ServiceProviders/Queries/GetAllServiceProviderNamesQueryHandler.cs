using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Application.Features.Vehicles.Queries;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Queries;

public class GetAllServiceProviderNamesQueryHandler : IQueryHandler<GetAllServiceProviderNamesQuery, Result<ServiceProviderNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllServiceProviderNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ServiceProviderNameListResponse>> Handle(GetAllServiceProviderNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var serviceProviderNames = await _dbContext.SetInt<ServiceProvider>()
                  .AsNoTracking()
                  .Select(e => new ServiceProviderNameResponse(
                          e.Id,
                          e.FirstName,
                          e.LastName))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ServiceProviderNameListResponse(serviceProviderNames);

                  return listResponse;
              });
    }
    #endregion

}