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

        List<int> visitLogIds = new List<int>();
        Dictionary<int, int> visitLogToUnitIdMap = new Dictionary<int, int>();

        foreach (var item in request.VisitorLogDetails)
        {
            var visitorLog = CreateVisitorLogEntity(item);
            _visitorLogRepository.Insert(visitorLog);
            visitLogIds.Add(visitorLog.Id);
            visitLogToUnitIdMap.Add(visitorLog.Id, item.VisitingUnitId);
        }

        if (visitLogIds.Any())
        {
            foreach (var id in visitLogIds)
            {
                int nextVisitorUnitLogId = _visitorUnitLogRepository.GetTotalRecordsCount() + 1;
                var visitingUnitId = visitLogToUnitIdMap[id];
                var visitorUnitLog = new VisitorUnitLog(nextVisitorUnitLogId, id, visitingUnitId);
                _visitorUnitLogRepository.Insert(visitorUnitLog);
            }
        }

        await _mediator.Publish(new VisitorLogCreatedEvent(request.VisitorId), cancellationToken);
        return Result.Success(new EntityCreatedResponse(request.VisitorId));
    }

    private VisitorLog CreateVisitorLogEntity(Contracts.VisitorLogs.VisitorLog visitorLog)
    {
        int nextVisitorLogId = _visitorLogRepository.GetTotalRecordsCount() + 1;
        return new VisitorLog(nextVisitorLogId, visitorLog.VisitorId, visitorLog.VisitingUnitId, visitorLog.VisitorTypeId, visitorLog.VisitingFrom, visitorLog.CurrentStatusId, visitorLog.EntryTime, visitorLog.ExitTime);
    }   
    #endregion
}