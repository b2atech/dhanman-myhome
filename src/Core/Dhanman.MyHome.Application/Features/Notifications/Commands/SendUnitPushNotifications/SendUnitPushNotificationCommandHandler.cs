using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain.Entities.UserFcmTokens;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Linq;

namespace Dhanman.MyHome.Application.Features.Notifications.Commands.SendUnitPushNotifications;

public class SendUnitPushNotificationCommandHandler
    : ICommandHandler<SendRequestApprovalActionCommand, Result<object>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFirebaseService _fcm;

    public SendUnitPushNotificationCommandHandler(IApplicationDbContext dbContext, IFirebaseService fcm)
    {
        _dbContext = dbContext;
        _fcm = fcm;
    }

    public async Task<Result<object>> Handle(SendRequestApprovalActionCommand request, CancellationToken cancellationToken)
    {

        const string sqlFunction = "SELECT * FROM public.get_user_ids_by_visitor_log_id(@p_VisitorLogId)";

        var visitorData = await _dbContext.SetInt<VisitorUserIdsDto>()
         //   .FromSqlRaw("SELECT * FROM public.GetUserIdsByVisitorLogId({visitorLogIdParam})" )
            .FromSqlRaw(sqlFunction, new NpgsqlParameter("p_VisitorLogId", request.VisitorLogId))
             .AsNoTracking()
                    .Select(e => new VisitorUserIdsResponse(
                        e.Id,
                        e.UserIds,
                        e.VisitorId,
                        e.FirstName,
                        e.LastName
                        ))
                    .ToListAsync(cancellationToken);

        if (visitorData == null || visitorData.Count == 0)
        {
            return Result.Failure<EntityCreatedResponse>(
                new Error("User.NotFound", $"No active users found for visitor log {request.VisitorLogId}")
            );
        }

        // Fix for CS1503: Ensure `userIds` is a collection of `Guid` and not a collection of collections.
        var userIds = visitorData.SelectMany(x => x.UserIds).ToList();

        var visitorName = string.Join(" ", visitorData.Select(x => x.FirstName).ToList(), visitorData.Select(x => x.LastName).ToList());

        var visitorid = visitorData.Select(x => x.VisitorId).ToList();


        if (!userIds.Any())
        {
            return Result.Failure<object>(
                new Error("User.NotFound", $"No active users found for unit {request.VisitorLogId}")
            );
        }

        // Step 2: Get their tokens
        var tokens = await _dbContext.SetInt<UserFcmToken>()
            .Where(x => userIds.Contains(x.UserId) && !string.IsNullOrWhiteSpace(x.FCMToken))
            .Select(x => x.FCMToken)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (!tokens.Any())
        {
            return Result.Failure<object>(
                new Error("UserFcmToken.NotFound", $"No valid FCM tokens found for unit {request.VisitorLogId}")
            );
        }

        // Step 3: Send notification to all tokens at once
        await _fcm.SendNotificationAsync(
            tokens,                               // ✅ list of tokens
            FirebaseMessageType.GateApprovalRequest,    // ✅ use your enum
            "Guest Approval Needed",              // title
            $"Guest {visitorName} is at the gate.",  // body
            new { GuestId = request.VisitorLogId }     // ✅ payload as object
        );

        return Result.Success<object>(new { SentCount = tokens.Count });
        
    }
}
