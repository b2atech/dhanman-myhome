using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public class GetAllVisitorNamesQueryHandler : IQueryHandler<GetAllVisitorNamesQuery, Result<VisitorNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllVisitorNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorNameListResponse>> Handle(GetAllVisitorNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var visitors = await _dbContext.SetInt<Visitor>()
                  .AsNoTracking()
                  .Select(e => new VisitorNameResponse(
                          e.Id,
                          e.FirstName,
                          e.LastName))
                  .ToListAsync(cancellationToken);

                  var listResponse = new VisitorNameListResponse(visitors);

                  return listResponse;
              });
    }
    #endregion

}