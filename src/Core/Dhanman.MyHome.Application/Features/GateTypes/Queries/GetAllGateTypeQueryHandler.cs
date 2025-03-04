using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.GateTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.GateTypes.Queries;

class GetAllGateTypeQueryHandler : IQueryHandler<GetAllGateTypeQuery, Result<GateTypeListResponse>>
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllGateTypeQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<GateTypeListResponse>> Handle(GetAllGateTypeQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var gateTypes = await _dbContext.SetInt<GateType>()
                  .AsNoTracking()
                  .Select(e => new GateTypeResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new GateTypeListResponse(gateTypes);

                  return listResponse;
              });
    }
    #endregion
}
