using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.VisitorLogs;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.MyHome.Domain.Entities.VisitorTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Queries;

public class GetAllVisitorLogsQueryHandler : IQueryHandler<GetAllVisitorLogsQuery, Result<VisitorLogListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllVisitorLogsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorLogListResponse>> Handle(GetAllVisitorLogsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  Guid apartmentId = _dbContext.SetInt<Visitor>()
                           .Where(e => e.Id == request.VisitorId)
                           .Select(e => e.ApartmentId)
                           .FirstOrDefault();
                  var visitorLogs = await _dbContext.SetInt<VisitorLog>()
                  .AsNoTracking()
                  .Where(e => apartmentId == query.ApartmentId && e.VisitorId == query.VisitorId && e.VisitorTypeId == query.VisitorTypeId)
                  .OrderBy(e => e.Id)
                  .Select(e => new VisitorLogResponse(
                          e.Id,
                          e.VisitorId,
                          _dbContext.SetInt<Visitor>()
                              .Where(p => p.Id == e.VisitorId)
                              .Select(p => p.FirstName).FirstOrDefault(),
                          e.VisitorTypeId,
                          _dbContext.SetInt<VisitorType>()
                              .Where(p => p.Id == e.VisitorTypeId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.VisitingFrom,
                          e.CurrentStatusId,
                          e.EntryTime,
                          e.ExitTime))
                  .ToListAsync(cancellationToken);

                  var listResponse = new VisitorLogListResponse(visitorLogs);

                  return listResponse;
              });
    }
    #endregion

}
