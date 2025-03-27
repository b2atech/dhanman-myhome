using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.CreateVisitorApproval;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Events;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

public class CreateVisitorApprovalCommandHandler : ICommandHandler<CreateVisitorApprovalCommand, Result<EntityCreatedResponse>>
{
    #region PRoerties
    private readonly IVisitorApprovalsRepository _visitorApprovalsRepository;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Construcor
    public CreateVisitorApprovalCommandHandler(IVisitorApprovalsRepository visitorApprovalsRepository, IApplicationDbContext dbContext, IMediator mediator)
    {
        _visitorApprovalsRepository = visitorApprovalsRepository;
        _dbContext = dbContext;
        _mediator = mediator;
    }
    #endregion

    #region Method
    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorApprovalCommand request, CancellationToken cancellationToken)
    {
        var parameters = new[]
        {
            new NpgsqlParameter("p_apartment_id", NpgsqlDbType.Uuid) { Value = request.ApartmentId },
            new NpgsqlParameter("p_first_name", NpgsqlDbType.Text) { Value = request.FirstName },
            new NpgsqlParameter("p_contact_number", NpgsqlDbType.Text) { Value = request.ContactNumber },
            new NpgsqlParameter("p_visitor_type_id", NpgsqlDbType.Integer) { Value = request.VisitorTypeId },
            new NpgsqlParameter("p_visit_type_id", NpgsqlDbType.Integer) { Value = request.VisitTypeId },
            new NpgsqlParameter("p_start_date", NpgsqlDbType.Date) { Value = request.StartDate },
            new NpgsqlParameter("p_end_date", NpgsqlDbType.Date) { Value = request.EndDate },
            new NpgsqlParameter("p_entry_time", NpgsqlDbType.Time) { Value = request.EntryTime },
            new NpgsqlParameter("p_exit_time", NpgsqlDbType.Time) { Value = request.ExitTime },
            new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) { Value = request.CreatedBy }
        };

        await _dbContext.Database.ExecuteSqlRawAsync(
            "SELECT * FROM public.save_visitor_and_approval(@p_apartment_id, @p_first_name, @p_contact_number, @p_visitor_type_id, @p_visit_type_id, " +
            "@p_start_date, @p_end_date, @p_entry_time, @p_exit_time, @p_created_by)",
            parameters
        );

        await _mediator.Publish(new VisitorApprovalCreatedEvent(request.VisitorApproveId), cancellationToken);
        return Result.Success(new EntityCreatedResponse(request.VisitorApproveId));
    }
    #endregion
}
