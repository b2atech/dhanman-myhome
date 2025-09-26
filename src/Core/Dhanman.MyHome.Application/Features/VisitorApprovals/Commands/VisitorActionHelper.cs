using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Abstractions;


internal static class VisitorActionHelper
{
    public static async Task<Result<UpdateVisitorStatusResponse>> TakeActionAsync(
        IVisitorRepository visitorRepository,
        IUserFcmTokenRepository fcmTokenRepository,
        IFirebaseService fcm,
        int visitorLogId,
        int unitId,
        Guid modifiedBy,
        string deviceId,
        VisitorStatus action,
        CancellationToken cancellationToken)
    {
        // Step 1: Repo call (approve/reject depending on action)
        var result = await visitorRepository.TakeVisitorActionAsync(
            visitorLogId,
            unitId,
            modifiedBy,
            action,
            cancellationToken);

        if (result == null)
            return Result.Failure<UpdateVisitorStatusResponse>(Errors.General.EntityNotFound);

        // Step 2: Build response
        var response = new UpdateVisitorStatusResponse
        {
            VisitorLogId = result.VisitorLogId,
            FinalStatusId = result.FinalStatusId
        };

        // Step 3: Notifications (awaited)
        var tokens = await fcmTokenRepository.GetUnitResidentFcmTokensAsync(unitId, cancellationToken);

        Console.WriteLine($"FcmTokens: {tokens}");
        if (tokens != null && tokens.Any())
        {
            var filtered = tokens
                .Where(t => t.ResidentId != result.ApproverResidentId ||
                            (t.ResidentId == result.ApproverResidentId && t.DeviceId != deviceId))
                .Select(t => t.FcmToken)
                .Distinct()
                .ToList();

            if (filtered.Any())
            {
                var approverName = $"{result.ApproverFirstName} {result.ApproverLastName}";
                var title = action == VisitorStatus.APPROVED
                    ? $"Visitor Approved by {approverName}"
                    : $"Visitor Rejected by {approverName}";
                var body = $"Action taken on visitor log {result.VisitorLogId}";

                var msgType = action == VisitorStatus.APPROVED
                    ? FirebaseMessageType.GateApproved
                    : FirebaseMessageType.GateRejected;

                var data = new Dictionary<string, string>
                {
                    { "VisitorLogId", visitorLogId.ToString() },
                    { "VisitorName", approverName },
                    { "UnitId", unitId.ToString() }
                };

                await fcm.SendNotificationAsync(filtered, msgType, title, body, data);
            }
        }

        return response;
    }
}
