using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.OccupancyTypes;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.OccupancyTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.OccupancyTypes.Queries;

public class GetAllOccupancyTypesQueryHandler : IQueryHandler<GetAllOccupancyTypesQuery, Result<OccupancyTypeListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllOccupancyTypesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<OccupancyTypeListResponse>> Handle(GetAllOccupancyTypesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var occupancyTypes = await _dbContext.SetInt<OccupancyType>()
                  .AsNoTracking()
                  .Select(e => new OccupancyTypeResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new OccupancyTypeListResponse(occupancyTypes);

                  return listResponse;
              });
    }
    #endregion

}