using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.ApproveVisitor;

public sealed class ApproveVisitorCommandHandler(
    IVisitorRepository visitorRepository,
    IUserFcmTokenRepository fcmTokenRepository,
    IFirebaseService fcm)
    : ICommandHandler<ApproveVisitorCommand, Result<UpdateVisitorStatusResponse>>
{
    public async Task<Result<UpdateVisitorStatusResponse>> Handle(ApproveVisitorCommand request, CancellationToken cancellationToken)
    {
        return await VisitorActionHelper.TakeActionAsync(
            visitorRepository,
            fcmTokenRepository,
            fcm,
            request.VisitorLogId,
            request.UnitId,
            request.ModifiedBy,
            request.DeviceId,
            VisitorStatus.APPROVED,
            cancellationToken);
    }
}
