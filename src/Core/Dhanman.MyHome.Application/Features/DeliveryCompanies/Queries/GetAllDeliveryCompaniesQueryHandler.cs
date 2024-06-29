using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.DeliveryCompanies;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.DeliveryCompanies;
using Dhanman.MyHome.Domain.Entities.DeliveryCompanyCategories;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.DeliveryCompanies.Queries;

public class GetAllDeliveryCompaniesQueryHandler : IQueryHandler<GetAllDeliveryCompaniesQuery, Result<DeliveryCompanyListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllDeliveryCompaniesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<DeliveryCompanyListResponse>> Handle(GetAllDeliveryCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
        .Bind(async query =>
        {
                  var deliveryCompanies = await _dbContext.SetInt<DeliveryCompany>()
                  .AsNoTracking()
                  .Select(e => new DeliveryCompanyResponse(
                          e.Id,  
                          e.Name,
                          e.DeliveryCompanyCategoryId,                          
                           _dbContext.SetInt<DeliveryCompanyCategory>()
                                  .Where(p => p.Id == e.DeliveryCompanyCategoryId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.Icon))
                  .ToListAsync(cancellationToken);

                  var listResponse = new DeliveryCompanyListResponse(deliveryCompanies);

                  return listResponse;
              });
    }
    #endregion

}