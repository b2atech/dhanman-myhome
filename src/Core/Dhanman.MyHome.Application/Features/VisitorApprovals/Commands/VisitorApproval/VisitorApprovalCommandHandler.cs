using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.VisitorApproval;



public class VisitorApprovalCommandHandler
: ICommandHandler<VisitorApprovalCommand, Result<object>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFirebaseService _fcm;

    public VisitorApprovalCommandHandler(IApplicationDbContext dbContext, IFirebaseService fcm)
    {
        _dbContext = dbContext;
        _fcm = fcm;
    }

    public async Task<Result<object>> Handle(VisitorApprovalCommand request, CancellationToken cancellationToken)
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
            return Result.Failure<EntityCreatedResponse>(
                new Error("User.NotFound", $"No active users found for visitor log {request.VisitorLogId}")
            );
        }

        var visitorName = $"{visitorData.First().FirstName} {visitorData.First().LastName}";
        var visitorId = visitorData.Select(x => x.VisitorId).ToList();

        var userIds = visitorData.SelectMany(x => x.UserIds).ToList();

        if (userIds.Count == 0)
        {
            return Result.Failure<object>(
                new Error("User.NotFound", $"No active users found for unit {request.VisitorLogId}")
            );
        }

        var tokens = visitorData.SelectMany(x => x.FcmTokens).ToList(); // Step 2: Get their tokens

        if (!tokens.Any())
        {
            return Result.Failure<object>(
                new Error("UserFcmToken.NotFound", $"No valid FCM tokens found for unit {request.VisitorLogId}")
            );
        }

        // Step 3: Send "approval request" notification to all tokens (existing logic, unchanged)
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

        // Step 4: Send "ApprovalDecision" notification after approval/rejection for multi-device sync
        // You need to know the decision and the actor ("by").
        // Replace the below lines with actual logic to determine decision and actor.
        var decision = "Approved"; // TODO: Get the real decision ("Approved" or "Rejected")
        var approvedBy = "John Doe"; // TODO: Get real user name/id who made the decision

        await _fcm.SendNotificationAsync(
            tokens,
            FirebaseMessageType.GateApprovalRequest, // <-- Use the enum, not a string
            "Visitor Approval Updated",
            $"Visitor was {decision} by {approvedBy}",
            new Dictionary<string, string>
            {
                { "AppData.Type", "ApprovalDecision" },
                { "AppData.VisitorLogId", request.VisitorLogId.ToString() },
                { "AppData.Decision", decision },
                { "AppData.By", approvedBy }
            }
        );

        return Result.Success<object>(new { SentCount = tokens.Count });
    }
}
