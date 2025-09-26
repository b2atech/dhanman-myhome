using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.RejectVisitor;

public sealed class RejectVisitorCommandHandler(
    IVisitorRepository visitorRepository,
    IUserFcmTokenRepository fcmTokenRepository,
    IFirebaseService fcm)
    : ICommandHandler<RejectVisitorCommand, Result<UpdateVisitorStatusResponse>>
{
    public async Task<Result<UpdateVisitorStatusResponse>> Handle(RejectVisitorCommand request, CancellationToken cancellationToken)
    {
        return await VisitorActionHelper.TakeActionAsync(
            visitorRepository,
            fcmTokenRepository,
            fcm,
            request.VisitorLogId,
            request.UnitId,
             Guid.Parse("21e44259-9472-4931-91d9-fc21cc43bb69"),
            "37baef73318bffd7",
            VisitorStatus.REJECTED,
            cancellationToken);
    }
}
