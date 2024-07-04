using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Dhanman.MyHome.Domain.Entities.ServiceProviderSubTypes;
using Dhanman.MyHome.Domain.Entities.ServiceProviderTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Queries;

public class GetAllServiceProvidersQueryHandler : IQueryHandler<GetAllServiceProvidersQuery, Result<ServiceProviderListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllServiceProvidersQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ServiceProviderListResponse>> Handle(GetAllServiceProvidersQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var serviceProviders = await _dbContext.SetInt<ServiceProvider>()
                  .AsNoTracking()
                  .Select(e => new ServiceProviderResponse(
                          e.Id,
                          e.FirstName,
                          e.LastName,
                          e.Email,
                          e.VisitingFrom,
                          e.ContactNumber,
                          e.PermanentAddressId,
                          e.PresentAddressId,
                          e.ServiceProviderTypeId,
                            _dbContext.SetInt<ServiceProviderType>()
                            .Where(p => p.Id == e.ServiceProviderTypeId)
                            .Select(p => p.Name).FirstOrDefault(),
                          e.ServiceProviderSubTypeId,
                            _dbContext.SetInt<ServiveProviderSubType>()
                            .Where(p => p.Id == e.ServiceProviderSubTypeId)
                            .Select(p => p.Name).FirstOrDefault(),
                          e.VehicleNumber,
                          e.IdentityTypeId,
                          e.IdentityNumber,  
                          e.Pin,
                          e.CreatedBy,
                          e.CreatedOnUtc,
                          e.ModifiedBy,
                          e.ModifiedOnUtc))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ServiceProviderListResponse(serviceProviders);

                  return listResponse;
              });
    }
    #endregion

} 
       
       
    