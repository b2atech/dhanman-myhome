using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.CommunityCalenders;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.CommunityCalenders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.CommunityCalenders.Queries;

public class GetAllCommunityCalenderNameQueryHandler : IQueryHandler<GetAllCommunityCalenderNameQuery, Result<CommunityCalenderNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllCommunityCalenderNameQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<CommunityCalenderNameListResponse>> Handle(GetAllCommunityCalenderNameQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var buildings = await _dbContext.SetInt<CommunityCalender>()
                  .AsNoTracking()
                  .OrderBy(e => e.Id)
                  .Select(e => new CommunityCalenderNameResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new CommunityCalenderNameListResponse(buildings);

                  return listResponse;
              });
    }
    #endregion

}
