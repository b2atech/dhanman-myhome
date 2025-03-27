using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ApprovedVisitors;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Features.ApprovedVisitors.Queries;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Queries;

internal sealed class GetVisitorApprovalInfoByIdQueryHandler : IQueryHandler<GetVisitorApprovalInfoByIdQuery, Result<VisitorApprovalsInfoByIdResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetVisitorApprovalInfoByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorApprovalsInfoByIdResponse>> Handle(GetVisitorApprovalInfoByIdQuery request, CancellationToken cancellationToken) =>
            await Result.Success(request)
                .Ensure(query => query.VisitorApprovalId > 0, Errors.General.EntityNotFound)
                .Bind(async query =>
                {
                    var visitorApproval = await _dbContext.SetInt<VisitorApprovalInfoById>()
                    .FromSqlRaw("SELECT * FROM public.get_visitor_approval_info_by_approval_id(@approval_id)",
                        new NpgsqlParameter("approval_id", request.VisitorApprovalId))
                    .AsNoTracking()
                    .Select(c => new VisitorApprovalsInfoByIdResponse(
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

                    if (visitorApproval == null)
                    {
                        return Result.Failure<VisitorApprovalsInfoByIdResponse>(Errors.General.EntityNotFound);
                    }


                    return visitorApproval;
                });

    #endregion
}
