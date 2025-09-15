using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.UserFcmTokens;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

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
        // Step 1: Get active resident IDs for this unit
        var userIds = await (
            from ru in _dbContext.SetInt<ResidentUnit>()
            join r in _dbContext.SetInt<Resident>() on ru.ResidentId equals r.Id
            where ru.UnitId == request.UnitId
                  && !ru.IsDeleted
                  && !r.IsDeleted
            select r.UserId
        ).ToListAsync(cancellationToken);

        if (!userIds.Any())
        {
            return Result.Failure<object>(
                new Error("User.NotFound", $"No active users found for unit {request.UnitId}")
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
                new Error("UserFcmToken.NotFound", $"No valid FCM tokens found for unit {request.UnitId}")
            );
        }

        // Step 3: Send notification to all tokens at once
        await _fcm.SendNotificationAsync(
            tokens,                               // ✅ list of tokens
            FirebaseMessageType.GateApprovalRequest,    // ✅ use your enum
            "Guest Approval Needed",              // title
            $"Guest {request.GuestName} is at the gate.",  // body
            new { GuestId = request.GuestId }     // ✅ payload as object
        );

        return Result.Success<object>(new { SentCount = tokens.Count });
        
    }
}
