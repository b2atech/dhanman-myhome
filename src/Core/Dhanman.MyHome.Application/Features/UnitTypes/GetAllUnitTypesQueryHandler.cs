using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.UnitTypes;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.UnitTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.UnitTypes
{
    public class GetAllUnitTypesQueryHandler : IQueryHandler<GetAllUnitTypesQuery, Result<UnitTypeListResponse>>
    {
        #region Properties
        private readonly IApplicationDbContext _dbContext;
        #endregion

        #region Constructors
        public GetAllUnitTypesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
        #endregion

        #region Methods
        public async Task<Result<UnitTypeListResponse>> Handle(GetAllUnitTypesQuery request, CancellationToken cancellationToken)
        {
            return await Result.Success(request)
                  .Ensure(query => query != null, Errors.General.EntityNotFound)
                  .Bind(async query =>
                  {
                      var unitTypes = await _dbContext.SetInt<UnitType>()
                      .AsNoTracking()
                      .Select(e => new UnitTypeResponse(
                              e.Id,
                              e.Name))
                      .ToListAsync(cancellationToken);

                      var listResponse = new UnitTypeListResponse(unitTypes);

                      return listResponse;
                  });
        }
        #endregion
    }
}
