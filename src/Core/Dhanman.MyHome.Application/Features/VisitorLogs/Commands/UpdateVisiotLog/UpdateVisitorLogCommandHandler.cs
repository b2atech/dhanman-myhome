using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.VisitorLogs.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.UpdateVisiotLog;

public class UpdateVisitorLogCommandHandler : ICommandHandler<UpdateVisitorLogCommand, Result<EntityUpdatedResponse>>
{
     #region Properties

        private readonly IVisitorLogRepository _visitorLogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public UpdateVisitorLogCommandHandler(IVisitorLogRepository visitorLogRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _visitorLogRepository = visitorLogRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>>Handle(UpdateVisitorLogCommand request, CancellationToken cancellationToken)
    {
        var existingVisitorLog = await _visitorLogRepository.GetByIntIdAsync(request.VisitorLogId);

        if (existingVisitorLog == null)
        {
            throw new VisitorNotFoundException(request.VisitorLogId);
        }

        existingVisitorLog.CurrentStatusId = request.CurrentStatusId;
        
        existingVisitorLog.ExitTime = request.ExitTime;

        existingVisitorLog.VisitorStatusId = request.VisitorStatusId;

        _visitorLogRepository.Update(existingVisitorLog);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new VisitorLogUpdatedEvent(request.VisitorLogId), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(existingVisitorLog.Id));
    }

    #endregion
}
