using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.IdendityTypes;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.IdentityTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.IdendityTypes.Queries;

class GetAllIdendityTypeQueryHandler : IQueryHandler<GetAllIdendityTypeQuery, Result<IdentityTypeListResponse>>
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllIdendityTypeQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<IdentityTypeListResponse>> Handle(GetAllIdendityTypeQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var identityTypes = await _dbContext.SetInt<IdentityType>()
                  .AsNoTracking()
                  .Select(e => new IdendityTypeResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new IdentityTypeListResponse(identityTypes);

                  return listResponse;
              });
    }
    #endregion
}
