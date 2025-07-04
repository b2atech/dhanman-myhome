using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;

public class UpdateVisitorApproveCommandHandler : ICommandHandler<UpdateVisitorApproveCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public UpdateVisitorApproveCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    #endregion

    #region Method
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateVisitorApproveCommand request, CancellationToken cancellationToken)
    {
        var parameters = new[]
        {
            new NpgsqlParameter("p_visitor_approval_id", NpgsqlDbType.Integer) { Value = request.VisitorApproveId },
            new NpgsqlParameter("p_visit_type_id", NpgsqlDbType.Integer) { Value = request.VisitTypeId },
            new NpgsqlParameter("p_start_date", NpgsqlDbType.Date) { Value = request.StartDate ?? (object)DBNull.Value },
            new NpgsqlParameter("p_end_date", NpgsqlDbType.Date) { Value = request.EndDate ?? (object)DBNull.Value },
            new NpgsqlParameter("p_entry_time", NpgsqlDbType.Time) { Value = request.EntryTime ?? (object)DBNull.Value },
            new NpgsqlParameter("p_exit_time", NpgsqlDbType.Time) { Value = request.ExitTime ?? (object)DBNull.Value },
            new NpgsqlParameter("p_vehicle_number", NpgsqlDbType.Text) { Value = string.IsNullOrEmpty(request.VehicleNumber) ? DBNull.Value : (object)request.VehicleNumber },
            new NpgsqlParameter("p_company_name", NpgsqlDbType.Text) { Value = string.IsNullOrEmpty(request.CompanyName) ? DBNull.Value : (object)request.CompanyName },
            new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) { Value = request.CreatedBy }
        };

        await _dbContext.Database.ExecuteSqlRawAsync(
            "SELECT * FROM public.update_visitor_approval(@p_visitor_approval_id, @p_visit_type_id, @p_start_date, @p_end_date, @p_entry_time, @p_exit_time, @p_vehicle_number, @p_company_name, @p_created_by)",
            parameters
        );

        return Result.Success(new EntityUpdatedResponse(request.VisitorApproveId));
    }
    #endregion
}
