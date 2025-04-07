using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.VisitorLogs;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Queries;

public class GetAllVisitorLogsQueryHandler : IQueryHandler<GetAllVisitorLogsQuery, Result<AllVisitorLogListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllVisitorLogsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<AllVisitorLogListResponse>> Handle(GetAllVisitorLogsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var allVisitorlogs = await _dbContext.SetInt<AllVisitorLog>()
                     .FromSqlRaw("SELECT * FROM public.get_visitor_multiple_unit_logs(@ApartmentId, @LogDate)",
                         new NpgsqlParameter("@ApartmentId", request.ApartmentId),
                         new NpgsqlParameter("@LogDate", request.Date)
                         )
                     .AsNoTracking()
                     .Select(c => new AllVisitorLogResponse(
                       c.Id,
                       c.VisitorId,
                       c.FirstName,
                       c.LastName ?? string.Empty,
                       c.UnitId,
                       c.UnitName ?? string.Empty,
                       c.LatestEntryTime,
                       c.LatestExitTime,
                       c.VisitingFrom ?? string.Empty,
                       c.ContactNumber ?? string.Empty,
                       c.VisitorTypeId,
                       c.VisitorTypeName ?? string.Empty
                         )).ToListAsync();


                  if (allVisitorlogs == null)
                  {
                      return Result.Failure<AllVisitorLogListResponse>(Errors.General.EntityNotFound);
                  }

                  return Result.Success(new AllVisitorLogListResponse(allVisitorlogs));
              });
    }
    #endregion
}

