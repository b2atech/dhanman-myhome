using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Tickets;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Dhanman.MyHome.Domain.Entities.ServiceProviderTicketCategories;
using Dhanman.MyHome.Domain.Entities.TicketCategories;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetAllServiceProviderTicketCategoryQueryHandler : IQueryHandler<GetAllServiceProviderTicketCategoryQuery, Result<ServiceProviderTicketCategoryListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetAllServiceProviderTicketCategoryQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<Result<ServiceProviderTicketCategoryListResponse>> Handle(GetAllServiceProviderTicketCategoryQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Bind(async query =>
              {
                  var serviceProviderCategories = await _dbContext.SetInt<ServiceProviderTicketCategory>()
                      .AsNoTracking()
                      .Where(spc => !spc.IsDeleted)
                      .Select(spc => new ServiceProviderTicketCategoryResponse(
                          spc.Id,
                          spc.TicketCategoryId,
                          _dbContext.SetInt<TicketCategory>()
                              .Where(tc => tc.Id == spc.TicketCategoryId)
                              .Select(tc => tc.Name)
                              .FirstOrDefault(),
                          spc.ServiceProviderId,
                          _dbContext.SetInt<ServiceProvider>()
                              .Where(sp => sp.Id == spc.ServiceProviderId)
                              .Select(sp => sp.FirstName + " " + sp.LastName)
                              .FirstOrDefault(),
                          _dbContext.SetInt<ServiceProvider>()
                              .Where(sp => sp.Id == spc.ServiceProviderId)
                              .Select(sp => sp.Email)
                              .FirstOrDefault(),
                          _dbContext.SetInt<ServiceProvider>()
                              .Where(sp => sp.Id == spc.ServiceProviderId)
                              .Select(sp => sp.ContactNumber)
                              .FirstOrDefault() 
                      ))
                      .ToListAsync(cancellationToken);

                 

                  var listResponse = new ServiceProviderTicketCategoryListResponse(serviceProviderCategories.AsReadOnly());

                  return listResponse;
              });
    }

    #endregion
}
