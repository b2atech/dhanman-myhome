using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.VisitorLogs;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.MyHome.Domain.Entities.VisitorStatuses;
using Dhanman.MyHome.Domain.Entities.VisitorTypes;
using Dhanman.MyHome.Domain.Entities.VisitorUnitLogs;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Queries;

public class GetSingleVisitorLogsQueryHandler : IQueryHandler<GetSingleVisitorLogsQuery, Result<VisitorLogListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetSingleVisitorLogsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorLogListResponse>> Handle(GetSingleVisitorLogsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  Guid apartmentId = _dbContext.SetInt<Visitor>()
                           .Where(e => e.Id == request.VisitorId)
                           .Select(e => e.ApartmentId)
                           .FirstOrDefault();

                  var visitorLogs = await (from vl in _dbContext.SetInt<VisitorLog>().AsNoTracking()
                                           join v in _dbContext.SetInt<Visitor>() on vl.VisitorId equals v.Id
                                           join vut in _dbContext.SetInt<VisitorUnitLog>() on vl.Id equals vut.VisitorLogId
                                           join u in _dbContext.SetInt<Unit>() on vut.UnitId equals u.Id
                                           where apartmentId == query.ApartmentId
                                               && vl.VisitorId == query.VisitorId
                                               && vl.VisitorTypeId == query.VisitorTypeId
                                           orderby vl.Id
                                           select new VisitorLogResponse(
                                               vl.Id,
                                               vl.VisitorId,
                                               v.FirstName,
                                               u.Id,
                                               u.Name,
                                               vl.VisitorTypeId,
                                               (from vt in _dbContext.SetInt<VisitorType>()
                                                where vt.Id == vl.VisitorTypeId
                                                select vt.Name).FirstOrDefault(),
                                               vl.VisitingFrom,
                                               vl.EntryTime,
                                               vl.ExitTime,
                                               vl.VisitorStatusId,
                                               (from vs in _dbContext.SetInt<VisitorStatus>()
                                                where vs.Id == vl.VisitorStatusId
                                                select vs.Name).FirstOrDefault())).ToListAsync(cancellationToken);

                  var listResponse = new VisitorLogListResponse(visitorLogs);

                  return listResponse;
              });
    }    
    #endregion

}
