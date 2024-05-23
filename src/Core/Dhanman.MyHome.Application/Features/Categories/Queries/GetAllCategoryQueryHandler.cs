using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Catergories;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Categories.Queries;

public class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryQuery, Result<CategoryListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllCategoryQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<CategoryListResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var categories = await _dbContext.SetInt<Category>()
                  .AsNoTracking()
                  .Select(c => new CategoryResponse(
                          c.Id,
                          c.Name
                        ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new CategoryListResponse(categories);

                  return listResponse;
              });
    }
    #endregion
}