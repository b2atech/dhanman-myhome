using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Floors;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Floors;
using Dhanman.MyHome.Domain;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetAllFloorNamesQueryHandler : IQueryHandler<GetAllFloorNamesQuery, Result<FloorNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllFloorNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<FloorNameListResponse>> Handle(GetAllFloorNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var floors = await _dbContext.SetInt<Floor>()
                  .AsNoTracking()
                  .Where(e => e.BuildingId == request.BuildingId)
                  .Select(e => new FloorNameResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new FloorNameListResponse(floors);

                  return listResponse;
              });
    }
    #endregion

}