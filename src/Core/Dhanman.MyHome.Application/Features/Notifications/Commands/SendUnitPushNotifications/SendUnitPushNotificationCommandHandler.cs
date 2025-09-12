using B2aTech.CrossCuttingConcern.Core.Primitives;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentTokens;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Notifications.Commands.SendUnitPushNotifications;

public class SendUnitPushNotificationCommandHandler
    : ICommandHandler<SendUnitPushNotificationCommand, Result<object>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFirebaseService _fcm;

    public SendUnitPushNotificationCommandHandler(IApplicationDbContext dbContext, IFirebaseService fcm)
    {
        _dbContext = dbContext;
        _fcm = fcm;
    }

    public async Task<Result<object>> Handle(SendUnitPushNotificationCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Get active resident IDs for this unit
        var residentIds = await (
            from ru in _dbContext.SetInt<ResidentUnit>()
            join r in _dbContext.SetInt<Resident>() on ru.ResidentId equals r.Id
            where ru.UnitId == request.UnitId
                  && !ru.IsDeleted
                  && !r.IsDeleted
            select r.Id
        ).ToListAsync(cancellationToken);

        if (!residentIds.Any())
        {
            return Result.Failure<object>(
                new Error("Resident.NotFound", $"No active residents found for unit {request.UnitId}")
            );
        }

        // Step 2: Get their tokens
        var tokens = await _dbContext.Set<ResidentToken>()
            .Where(x => residentIds.Contains(x.ResidentId) && !string.IsNullOrWhiteSpace(x.FCMToken))
            .Select(x => x.FCMToken)
            .ToListAsync(cancellationToken);

        if (!tokens.Any())
        {
            return Result.Failure<object>(
                new Error("ResidentToken.NotFound", $"No valid FCM tokens found for unit {request.UnitId}")
            );
        }

        // Step 3: Send notification to each resident
        foreach (var token in tokens)
        {
            await _fcm.SendNotificationAsync(
                token,
                "Guest Approval Needed",
                $"Guest {request.GuestName} is at the gate.",
                request.GuestId
            );
        }

        return Result.Success<object>(new { SentCount = tokens.Count });
    }
}
