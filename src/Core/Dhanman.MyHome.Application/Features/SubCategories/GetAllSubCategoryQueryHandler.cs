using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ServiceProviderTypes;
using Dhanman.MyHome.Application.Contracts.SubCategories;
using Dhanman.MyHome.Application.Features.ServiceProviderTypes.Queries;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.SubCategories;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.SubCategories;

internal class GetAllSubCategoryQueryHandler : IQueryHandler<GetAllSubCategoryQuery, Result<SubCategoryListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllSubCategoryQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<SubCategoryListResponse>> Handle(GetAllSubCategoryQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var subCategories = await _dbContext.SetInt<SubCategory>()
                  .AsNoTracking()
                  .Select(e => new SubCategoryResponse(
                          e.Id,
                          e.Name,
                          e.CategoryId
                          ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new SubCategoryListResponse(subCategories);

                  return listResponse;
              });
    }
    #endregion
}

