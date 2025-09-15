using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain.Entities.ResidentTokens;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Notifications.Commands.SendPushNotifications;

public class SendPushNotificationCommandHandler : ICommandHandler<SendPushNotificationCommand, Result<object>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IFirebaseService _fcm;
    #endregion

    #region Constructor
    public SendPushNotificationCommandHandler(IApplicationDbContext dbContext, IFirebaseService fcm)
    {
        _dbContext = dbContext;
        _fcm = fcm;
    }
    #endregion

    #region Methods
    public async Task<Result<object>> Handle(SendPushNotificationCommand request, CancellationToken cancellationToken)
    {
        var residentToken = await _dbContext.Set<ResidentToken>()
               .Where(x => x.ResidentId == request.ResidentId)
        .FirstOrDefaultAsync(cancellationToken);


        // Example tokens
        var tokens = new List<string> { "token1", "token2", "token3" };

        // Example payload object
        var payload = new
        {
            guestId = request.GuestId,
            guestName = request.GuestName,
            initiator = "Security Guard",
            requestTime = DateTime.UtcNow.ToString("o")
        };

        // Call the service
        await _fcm.SendNotificationAsync(
            tokens,
            FirebaseMessageType.GateApprovalRequest,
            "Guest Approval Needed",
            "You have a guest request from the gate.",
            payload
        );

        //if (residentToken == null || string.IsNullOrEmpty(residentToken.FCMToken))
        //    throw new Exception("Resident or FCM token not found");

        ////var residentToken = "e13g5evXSV6s_nQBMmmSrl:APA91bGCdc2tSrA0Tp4ZXZOMLjH8Qw8bwn8oRrNTOS1yq9jmflsy4VcEoXtApRH2S4OtB1cHjPr_FgnPIFVwRBMhQBObvKzKwx3GegNysrlSi2dkskTjWzE";

        //await _fcm.SendNotificationAsync(
        //    residentToken.FCMToken,
        //    "Guest Approval Needed",
        //    $"Guest {request.GuestName} is at the gate.",
        //     request.GuestId 
        //);
        return Unit.Value;
    }

    #endregion
}
