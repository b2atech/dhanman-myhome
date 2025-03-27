using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.MemberRequests;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using Dhanman.MyHome.Domain.Entities.ResidentStatuses;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Queries;

public class GetAllMemberRequestsQueryHandler : IQueryHandler<GetAllMemberRequestsQuery, Result<MemberRequestListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllMemberRequestsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods

    public async Task<Result<MemberRequestListResponse>> Handle(GetAllMemberRequestsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var memberRequests = await _dbContext.SetInt<ResidentRequest>()
                  .AsNoTracking()
                  .Where(e => e.ApartmentId == request.ApartmentId)
                  .Select(e => new MemberRequestResponse(
                          e.Id,
                          e.ApartmentId,
                          _dbContext.Set<Apartment>()
                                  .Where(p => p.Id == e.ApartmentId)
                                  .Select(p => p.Name).FirstOrDefault(),                          
                          e.FirstName,
                          e.LastName,                          
                          e.Email,
                          e.ContactNumber,                         
                          e.RequestStatusId,
                          _dbContext.SetInt<ResidentRequestStatus>()
                                  .Where(p => p.Id == e.RequestStatusId)
                                  .Select(p => p.Name).FirstOrDefault()                         
                          ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new MemberRequestListResponse(memberRequests);

                  return listResponse;
              });
    }
    #endregion

}