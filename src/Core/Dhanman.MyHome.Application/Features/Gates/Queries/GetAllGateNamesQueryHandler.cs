using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Domain.Entities.Gates;
using Dhanman.MyHome.Domain;

namespace Dhanman.MyHome.Application.Features.Gates.Queries;

public class GetAllGateNamesQueryHandler : IQueryHandler<GetAllGateNamesQuery, Result<GateNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllGateNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<GateNameListResponse>> Handle(GetAllGateNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var gates = await _dbContext.SetInt<Gate>()
                  .AsNoTracking()
                  .Select(e => new GateNameResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new GateNameListResponse(gates);

                  return listResponse;
              });
    }
    #endregion

}