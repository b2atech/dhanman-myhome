using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Features.VisitorLogs.Events;
using Dhanman.MyHome.Application.Features.Visitors.Events;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitorAndLog;

public class CreateVisitorWithPendingApprovalCommandHandler : ICommandHandler<CreateVisitorWithPendingApprovalCommand, Result<EntityCreatedResponse>>
{
    #region PRoerties
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    #endregion

    #region Construcor
    public CreateVisitorWithPendingApprovalCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    #endregion

    #region Method
    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorWithPendingApprovalCommand request, CancellationToken cancellationToken)
    {
        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT public.create_visitor_with_pending_approval(@p_apartment_id, @p_first_name, @p_last_name, @p_contact_number, @p_visitor_type_id, @p_unit_ids, @p_created_by)";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_apartment_id", NpgsqlDbType.Uuid) { Value = request.ApartmentId });
        command.Parameters.Add(new NpgsqlParameter("p_first_name", NpgsqlDbType.Text) { Value = request.FirstName });
        command.Parameters.Add(new NpgsqlParameter("p_last_name", NpgsqlDbType.Text) { Value = (object?)request.LastName ?? DBNull.Value });
        command.Parameters.Add(new NpgsqlParameter("p_contact_number", NpgsqlDbType.Text) { Value = request.ContactNumber });
        command.Parameters.Add(new NpgsqlParameter("p_visitor_type_id", NpgsqlDbType.Integer) { Value = request.VisitorTypeId });
        command.Parameters.Add(new NpgsqlParameter("p_unit_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer)
        {
            Value = request.UnitIds.ToArray()
        });
        command.Parameters.Add(new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) { Value = request.CreatedBy });

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result is not int visitorLogId)
        {
            return Result.Failure<EntityCreatedResponse>(Errors.General.EntityNotFound);
        }

        await _mediator.Publish(new VisitorLogCreatedEvent(visitorLogId), cancellationToken);

        return Result.Success(new EntityCreatedResponse(visitorLogId));

    }

    #endregion
}

