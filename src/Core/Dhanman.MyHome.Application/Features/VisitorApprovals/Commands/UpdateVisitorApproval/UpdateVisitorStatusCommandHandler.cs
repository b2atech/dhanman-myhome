using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;

public sealed class UpdateVisitorStatusCommandHandler(IApplicationDbContext _dbContext, IFirebaseService _fcm)
    : ICommandHandler<UpdateVisitorStatusCommand, Result<UpdateVisitorStatusResponse>>
{
    #region Methods
    public async Task<Result<UpdateVisitorStatusResponse>> Handle(UpdateVisitorStatusCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await UpdateStatusAsync(request, cancellationToken);
        if (updateResult.IsFailure)
        {
            return Result.Failure<UpdateVisitorStatusResponse>(updateResult.Error);
        }

        _ = Task.Run(() => SendVisitorStausNotificationsAsync(request, cancellationToken), cancellationToken);

        return new UpdateVisitorStatusResponse();
    }

    #endregion

    #region Private Methods
    private async Task<Result<UpdateVisitorStatusResponse>> UpdateStatusAsync(UpdateVisitorStatusCommand request, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM public.update_visitor_status(@p_visitor_log_id, @p_unit_id, @p_visitor_status_id, @p_modified_by)";

        var parameters = new[]
        {
            new NpgsqlParameter("p_visitor_log_id", request.VisitorLogId),
            new NpgsqlParameter("p_unit_id", request.UnitId),
            new NpgsqlParameter("p_visitor_status_id", request.VisitorStatusId),
            new NpgsqlParameter("p_modified_by", request.ModifiedBy)
        };

        var responseDto = await _dbContext.SetInt<UpdateVisitorStatusDto>()
             .FromSqlRaw(sql, parameters)
            .AsNoTracking()
            .Select(r => new UpdateVisitorStatusResponse
            {
                VisitorLogId = r.VisitorId,
                TotalVisitsCount = r.TotalVisitsCount,
                ApprovedVisitsCount = r.ApprovedVisitsCount,
                FinalStatusId = r.FinalStatusId

            })
            .FirstOrDefaultAsync(cancellationToken);

        if (responseDto == null)
        {
            return Result.Failure<UpdateVisitorStatusResponse>(Errors.General.EntityNotFound);
        }

        return responseDto;
    }

    private async Task SendVisitorStausNotificationsAsync(UpdateVisitorStatusCommand request, CancellationToken cancellationToken)
    {
        const string sqlFunction = "SELECT * FROM public.get_resident_fcm_tokens_by_visitor_log_id(@p_visitorlogid)";

        var notificationData = await _dbContext.SetInt<VisitorNotificationDataDto>()
            .FromSqlRaw(sqlFunction, new NpgsqlParameter("p_visitorlogid", request.VisitorLogId))
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (notificationData == null || !notificationData.FcmTokens.Any())
        {
            return;
        }

        var fcmTokens = notificationData.FcmTokens.Distinct().ToList();

        var visitorName = $"{notificationData.FirstName} {notificationData.LastName}";
        var title = request.VisitorStatusId == 2 ? $"Visitor Approved: {visitorName}" : $"Visitor Rejected: {visitorName}";
        var body = $"An approval action was taken for visitor {visitorName}.";
        var fireBaseMsgType = request.VisitorStatusId == 2 ? FirebaseMessageType.GateApproved 
                            : (request.VisitorStatusId == 6
                            ? FirebaseMessageType.GateRejected
                            : FirebaseMessageType.SystemAnnouncement);

        var data = new Dictionary<string, string>
        {
            { "VisitorLogId", request.VisitorLogId.ToString() },
            { "VisitorName", visitorName },
            { "UnitId", request.UnitId.ToString() }
        };

        await _fcm.SendNotificationAsync(
            fcmTokens,
            fireBaseMsgType,
            title,
            body,
            data
            );
    }
    #endregion
}

