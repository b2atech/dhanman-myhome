using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.IdentityTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public class GetVisitorIdentityTypesQueryHandler : IQueryHandler<GetVisitorIdentityTypesQuery, Result<VisitorIdentityTypeListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetVisitorIdentityTypesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorIdentityTypeListResponse>> Handle(GetVisitorIdentityTypesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var visitorIdentityTypes = await _dbContext.SetInt<IdentityType>()
                  .AsNoTracking()
                  .Select(e => new VisitorIdentityTypeResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new VisitorIdentityTypeListResponse(visitorIdentityTypes);

                  return listResponse;
              });
    }
    #endregion

}