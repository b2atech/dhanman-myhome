using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Residents;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Residents.Queries;

public class GetAllResidentNamesQueryHandler : IQueryHandler<GetAllResidentNamesQuery, Result<ResidentNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllResidentNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ResidentNameListResponse>> Handle(GetAllResidentNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var residents = await _dbContext.SetInt<Resident>()
                  .AsNoTracking()
                  .Where(e => e.ApartmentId == request.ApartmentId)
                  .Select(e => new ResidentNameResponse(
                          e.Id,                           
                          e.FirstName,
                          e.LastName,
                          e.UserId))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ResidentNameListResponse(residents);

                  return listResponse;
              });
    }
    #endregion

}
