using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.VisitorTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public class GetVisitorTypesQueryHandler : IQueryHandler<GetVisitorTypesQuery, Result<VisitorTypeListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetVisitorTypesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorTypeListResponse>> Handle(GetVisitorTypesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var visitorTypes = await _dbContext.SetInt<VisitorType>()
                  .AsNoTracking()
                  .Select(e => new VisitorTypeResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new VisitorTypeListResponse(visitorTypes);

                  return listResponse;
              });
    }
    #endregion

}