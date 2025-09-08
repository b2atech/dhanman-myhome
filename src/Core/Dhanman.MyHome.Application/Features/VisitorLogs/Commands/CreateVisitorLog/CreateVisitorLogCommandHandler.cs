using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
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
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor

    public CreateVisitorLogCommandHandler(IVisitorLogRepository visitorLogRepository, IVisitorUnitLogRepository visitorUnitLogRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _visitorLogRepository = visitorLogRepository;
        _visitorUnitLogRepository = visitorUnitLogRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods

    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorLogCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Create and save VisitorLog
        var visitorLog = CreateVisitorLogEntity(request);
        _visitorLogRepository.Insert(visitorLog);

        // Save so visitorLog.Id is generated
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Step 2: Insert VisitorUnitLogs with valid visitorLog.Id
        foreach (var unitId in request.VisitingUnitIds)
        {
            var visitorUnitLog = new VisitorUnitLog(visitorLog.Id, unitId);
            _visitorUnitLogRepository.Insert(visitorUnitLog);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Step 3: Publish event
        await _mediator.Publish(new VisitorLogCreatedEvent(request.VisitorId), cancellationToken);

        return Result.Success(new EntityCreatedResponse(visitorLog.Id));
    }

    private VisitorLog CreateVisitorLogEntity(CreateVisitorLogCommand visitorLog)
    {
        //int nextVisitorLogId = _visitorLogRepository.GetTotalRecordsCount() + 1;
        return new VisitorLog(visitorLog.VisitorId, visitorLog.VisitorTypeId, visitorLog.VisitingFrom, visitorLog.EntryTime, visitorLog.ExitTime, visitorLog.VisitorStatusId);
    }   
    #endregion
}