using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.VisitorApproval;

public class VisitorApprovalCommandHandler
    : ICommandHandler<VisitorApprovalCommand, Result<VisitorApprovalNotificationResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFirebaseService _fcm;

    public VisitorApprovalCommandHandler(IApplicationDbContext dbContext, IFirebaseService fcm)
    {
        _dbContext = dbContext;
        _fcm = fcm;
    }

    public async Task<Result<VisitorApprovalNotificationResponse>> Handle(VisitorApprovalCommand request, CancellationToken cancellationToken)
    {

        const string sqlFunction = "SELECT * FROM public.visitor_approval_fcm_tokens_by_visitor_log_id(@p_VisitorLogId)";

        var visitorData = await _dbContext.SetInt<VisitorUserIdsDto>()
            .FromSqlRaw(sqlFunction, new NpgsqlParameter("p_VisitorLogId", request.VisitorLogId))
             .AsNoTracking()
                    .Select(e => new VisitorUserIdsResponse(
                        e.Id,
                        e.UserIds,
                        e.VisitorId,
                        e.FirstName,
                        e.LastName,
                        e.FcmTokens,
                        e.DeviceIds
                        ))
                    .ToListAsync(cancellationToken);

        if (visitorData == null || visitorData.Count == 0)
        {
            return Result.Failure<VisitorApprovalNotificationResponse>(
                new Error("User.NotFound", $"No active users found for visitor log {request.VisitorLogId}")
            );
        }

        var visitorName = $"{visitorData.First().FirstName} {visitorData.First().LastName}";
        var visitorId = visitorData.Select(x => x.VisitorId).ToList();

        var userIds = visitorData.SelectMany(x => x.UserIds).ToList();

        if (userIds.Count == 0)
        {
            return Result.Failure<VisitorApprovalNotificationResponse>(
                new Error("User.NotFound", $"No active users found for unit {request.VisitorLogId}")
            );
        }

        var tokens = visitorData.SelectMany(x => x.FcmTokens).ToList(); // Step 2: Get their tokens

        if (!tokens.Any())
        {
            return Result.Failure<VisitorApprovalNotificationResponse>(
                new Error("UserFcmToken.NotFound", $"No valid FCM tokens found for unit {request.VisitorLogId}")
            );
        }

        // Step 3: Send notification to all tokens at once
        await _fcm.SendNotificationAsync(
            tokens,
            FirebaseMessageType.GateApprovalRequest,
            "Guest Approval Needed",
            $"Guest {visitorName} is at the gate.",
            new Dictionary<string, string>
            {
                { "AppData.GuestId", string.Join(",", visitorId) },
                { "AppData.VisitorName", visitorName },
                { "AppData.VisitorLogId", request.VisitorLogId.ToString() },
                { "AppData.Type", FirebaseMessageType.GateApprovalRequest.ToString() },
                { "AppData.Version", "1.0" }
            }
        );

        // return new response object
        return
            new VisitorApprovalNotificationResponse(
                tokens.Count,
                visitorData.First().VisitorId,
                visitorData.First().FirstName,
                 visitorData.First().LastName ?? string.Empty
            );
    }
}
