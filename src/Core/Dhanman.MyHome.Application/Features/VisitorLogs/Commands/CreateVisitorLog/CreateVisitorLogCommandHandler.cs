using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.VisitorLogs.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.VisitorLogs;
using Dhanman.MyHome.Domain.Entities.VisitorUnitLogs;
using MediatR;



namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.CreateVisitorLog;

public class CreateVisitorLogCommandHandler : ICommandHandler<CreateVisitorLogCommand, Result<EntityCreatedResponse>>
{

    #region Properties

    private readonly IVisitorLogRepository _visitorLogRepository;
    private readonly IVisitorUnitLogRepository _visitorUnitLogRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor

    public CreateVisitorLogCommandHandler(IVisitorLogRepository visitorLogRepository, IVisitorUnitLogRepository visitorUnitLogRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _visitorLogRepository = visitorLogRepository;
        _visitorUnitLogRepository = visitorUnitLogRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods

    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorLogCommand request, CancellationToken cancellationToken)
    {
        var visitorLog = CreateVisitorLogEntity(request);
        _visitorLogRepository.Insert(visitorLog);
        int nextVisitorUnitLogId = _visitorUnitLogRepository.GetTotalRecordsCount();
        foreach (var unitId in request.VisitingUnitIds)
        {
            nextVisitorUnitLogId = nextVisitorUnitLogId + 1;            
            var visitorUnitLog = new VisitorUnitLog(nextVisitorUnitLogId, visitorLog.Id, unitId);
            _visitorUnitLogRepository.Insert(visitorUnitLog);
        }
        
        await _mediator.Publish(new VisitorLogCreatedEvent(request.VisitorId), cancellationToken);
        return Result.Success(new EntityCreatedResponse(request.VisitorId));
    }

    private VisitorLog CreateVisitorLogEntity(CreateVisitorLogCommand visitorLog)
    {
        int nextVisitorLogId = _visitorLogRepository.GetTotalRecordsCount() + 1;
        return new VisitorLog(nextVisitorLogId, visitorLog.VisitorId, visitorLog.VisitorTypeId, visitorLog.VisitingFrom, visitorLog.CurrentStatusId, visitorLog.EntryTime, visitorLog.ExitTime);
    }   
    #endregion
}