using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.CreateVisitorApproval;

public class CreateVisitorApprovalCommandHandler : ICommandHandler<CreateVisitorApprovalCommand, Result<EntityCreatedResponse>>
{
    private readonly IVisitorApprovalsRepository _visitorApprovalsRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;


    public CreateVisitorApprovalCommandHandler(IVisitorApprovalsRepository visitorApprovalsRepository, IApplicationDbContext dbContext, IMediator mediator)
    {
        _visitorApprovalsRepository = visitorApprovalsRepository;
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorApprovalCommand request, CancellationToken cancellationToken)
    {
        // Call your PL/pgSQL function here (save_visitor_and_approval)
        await _dbContext.Database.ExecuteSqlRawAsync("public.save_visitor_and_approval",
            new
            {
                p_apartment_id = request.ApartmentId,
                p_first_name = request.FirstName,
                p_contact_number = request.ContactNumber,
                p_visitor_type_id = request.VisitTypeId,  // Assuming it's a visitor type ID
                p_visit_type_id = request.VisitTypeId,
                p_start_date = request.StartDate,
                p_end_date = request.EndDate,
                p_entry_time = request.EntryTime,
                p_exit_time = request.ExitTime,
                p_created_by = request.CreatedBy
            });

        await _mediator.Publish(new VisitorApprovalCreatedEvent(request.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(request.Id));
        }
}
