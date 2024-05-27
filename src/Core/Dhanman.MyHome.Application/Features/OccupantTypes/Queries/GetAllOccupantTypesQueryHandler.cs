using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.OccupantTypes;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.OccupantTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.OccupantTypes.Queries
{
    public class GetAllOccupantTypesQueryHandler : IQueryHandler<GetAllOccupantTypesQuery, Result<OccupantTypeListResponse>>
    {

        #region Properties
        private readonly IApplicationDbContext _dbContext;
        #endregion

        #region Constructors
        public GetAllOccupantTypesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

        #endregion

        #region Methods
        public async Task<Result<OccupantTypeListResponse>> Handle(GetAllOccupantTypesQuery request, CancellationToken cancellationToken)
        {
            return await Result.Success(request)
                  .Ensure(query => query != null, Errors.General.EntityNotFound)
                  .Bind(async query =>
                  {
                      var occupantTypes = await _dbContext.SetInt<OccupantType>()
                      .AsNoTracking()
                      .Select(e => new OccupantTypeResponse(
                              e.Id,
                              e.Name))
                      .ToListAsync(cancellationToken);

                      var listResponse = new OccupantTypeListResponse(occupantTypes);

                      return listResponse;
                  });
        }
        #endregion
    }
}
