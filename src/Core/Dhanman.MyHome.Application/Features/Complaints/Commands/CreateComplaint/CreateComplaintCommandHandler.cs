using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Complaints.Events;
using Dhanman.MyHome.Application.Features.Events.Commands.CreateComplaint;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Complaints;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Complaints.Commands.CreateComplaint;

public class CreateComplaintCommandHandler : ICommandHandler<CreateComplaintCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IComplaintRepository _complaintRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateComplaintCommandHandler(IComplaintRepository complaintRepository, IMediator mediator)
    {
        _complaintRepository = complaintRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
    {
        var complaintRequest = new Complaint(
            request.Id,
            request.Subject,
            request.Description, 
            request.DocLink, 
            request.PrefferedTime,
            request.CategoryId,
            request.SubCategoryId,
            request.PriorityId,
            request.DepartmentId,
            request.OccuredDate,
            request.PrefferedDate,
            request.IsUrgent
        );

        _complaintRepository.Insert(complaintRequest);
        await _mediator.Publish(new ComplaintCreatedEvent(complaintRequest.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(complaintRequest.Id));
    }
    #endregion
}