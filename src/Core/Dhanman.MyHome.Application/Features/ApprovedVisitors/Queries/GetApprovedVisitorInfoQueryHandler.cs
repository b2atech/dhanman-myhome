using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ApprovedVisitors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.ApprovedVisitors;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.ApprovedVisitors.Queries;

internal sealed class GetApprovedVisitorInfoQueryHandler : IQueryHandler<GetApprovedVisitorInfoQuery, Result<ApprovedInfoByIdResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetApprovedVisitorInfoQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ApprovedInfoByIdResponse>> Handle(GetApprovedVisitorInfoQuery request, CancellationToken cancellationToken) =>
            await Result.Success(request)
                .Ensure(query => query.ApprovedVisitorId > 0, Errors.General.EntityNotFound)
                .Bind(async query =>
                {
                    var customer = await _dbContext.SetInt<ApprovedVisitorInfoById>()
                    .FromSqlRaw("SELECT * FROM public.get_visitor_approval_info_by_approval_id(@approval_id)",
                        new NpgsqlParameter("approval_id", request.ApprovedVisitorId))
                    .AsNoTracking()
                    .Select(c => new ApprovedInfoByIdResponse(
                        c.Visitor_Id,
                        c.First_Name,
                        c.Last_Name ?? string.Empty,
                        c.Contact_Number ?? string.Empty,
                        c.StartDate,
                        c.EndDate,
                        c.EntryTime,
                        c.ExitTime,
                        c.CreatedByFirstName ?? string.Empty,
                        c.CreatedByLastName ?? string.Empty,
                        c.UnitName ?? string.Empty,
                        c.ApartmentName ?? string.Empty,
                        c.CityName ?? string.Empty
                        ))
                    .FirstOrDefaultAsync(cancellationToken);

                    if (customer == null)
                    {
                        return Result.Failure<ApprovedInfoByIdResponse>(Errors.General.EntityNotFound);
                    }


                    return customer;
                });

    #endregion
}
