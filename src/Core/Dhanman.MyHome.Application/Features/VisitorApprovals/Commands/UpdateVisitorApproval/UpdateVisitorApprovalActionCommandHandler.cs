using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.UserFcmTokens;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public sealed class UpdateVisitorApprovalActionCommandHandler
    : ICommandHandler<UpdateVisitorApprovalActionCommand, Result<ApprovalActionResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFirebaseService _fcm;

    public UpdateVisitorApprovalActionCommandHandler(IApplicationDbContext dbContext, IFirebaseService fcm)
    {
        _dbContext = dbContext;
        _fcm = fcm;
    }

    public async Task<Result<ApprovalActionResponse>> Handle(UpdateVisitorApprovalActionCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Update the approval status in the database.
        var updateResult = await UpdateApprovalStatusAsync(request, cancellationToken);

        if (updateResult.IsFailure)
        {
            return Result.Failure<ApprovalActionResponse>(updateResult.Error);
        }

        // Step 2: Asynchronously gather data and send notifications without blocking the response.
        // Using Task.Run to avoid making the user wait for notifications to be sent.
        _ = Task.Run(() => SendApprovalNotificationsAsync(request, cancellationToken), cancellationToken);

        // Step 3: Return the successful response immediately.
        return updateResult;
    }

    /// <summary>
    /// Calls the database function to update the visitor approval status.
    /// </summary>
    private async Task<Result<ApprovalActionResponse>> UpdateApprovalStatusAsync(UpdateVisitorApprovalActionCommand request, CancellationToken cancellationToken)
    {
        const string sql = "SELECT public.response_over_approval_action(@p_visitor_log_id, @p_unit_id, @p_visitor_status_id, @p_modified_by)";

        var parameters = new[]
        {
            new NpgsqlParameter("p_visitor_log_id", request.VisitorLogId),
            new NpgsqlParameter("p_unit_id", request.UnitId),
            new NpgsqlParameter("p_visitor_status_id", request.VisitorStatusId),
            new NpgsqlParameter("p_modified_by", request.ModifiedBy)
        };

        var responseDto = await _dbContext.SetInt<ResponseOverApprovalActionDto>()
            .FromSqlRaw(sql, parameters)
            .AsNoTracking()
            .Select(UnitId => new ResponseOverApprovalActionDto
            {
                Success = UnitId.Success
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (responseDto == null || !responseDto.Success)
        {
            return Result.Failure<ApprovalActionResponse>(Errors.General.EntityNotFound);
        }

        return new ApprovalActionResponse(responseDto.Success);
    }

    /// <summary>
    /// Gathers recipient data and sends push notifications.
    /// </summary>
    private async Task SendApprovalNotificationsAsync(UpdateVisitorApprovalActionCommand request, CancellationToken cancellationToken)
    {
        // Get FCM tokens for other residents of the same unit.
        var residentTokens = await GetResidentFcmTokensAsync(request.UnitId, request.ModifiedBy, cancellationToken);

        // Here you can also fetch tokens for security staff if needed
        // var securityTokens = await GetSecurityFcmTokensAsync(cancellationToken);

        var title = request.VisitorStatusId == 2 ? "Visitor Approved" : "Visitor Rejected";
        var body = $"Action taken on a visitor for your unit.";
        var fireBaseMsgType = request.VisitorStatusId == 2 ? FirebaseMessageType.GateApproved : FirebaseMessageType.GateRejected;

        if (residentTokens.Any())
        {
            await _fcm.SendNotificationAsync(
                residentTokens,
                fireBaseMsgType,
                title,
                body,
                new { VisitorLogId = request.VisitorLogId, UnitId = request.UnitId });
        }
    }

    /// <summary>
    /// Fetches FCM tokens for all residents of a specific unit, excluding the user who took the action.
    /// </summary>
    private async Task<List<string>> GetResidentFcmTokensAsync(int unitId, Guid modifiedBy, CancellationToken cancellationToken)
    {
        var residentUserIds = await _dbContext.SetInt<ResidentUnit>()
            .Where(ru => ru.UnitId == unitId && !ru.IsDeleted)
            .Join(_dbContext.SetInt<Resident>(),
                  ru => ru.ResidentId,
                  r => r.Id,
                  (ru, r) => r)
            .Where(r => !r.IsDeleted && r.UserId != modifiedBy)
            .Select(r => r.UserId)
            .ToListAsync(cancellationToken);

        if (!residentUserIds.Any())
        {
            return new List<string>();
        }

        return await _dbContext.SetInt<UserFcmToken>()
            .Where(t => residentUserIds.Contains(t.UserId) && !string.IsNullOrWhiteSpace(t.FCMToken))
            .Select(t => t.FCMToken)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
