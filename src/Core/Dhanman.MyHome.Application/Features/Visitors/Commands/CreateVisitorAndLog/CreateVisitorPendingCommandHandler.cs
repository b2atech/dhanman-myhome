using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Visitors.Events;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitorAndLog;

public class CreateVisitorPendingCommandHandler : ICommandHandler<CreateVisitorPendingCommand, Result<EntityCreatedResponse>>
{
    #region PRoerties
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    #endregion

    #region Construcor
    public CreateVisitorPendingCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    #endregion

    #region Method
    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorPendingCommand request, CancellationToken cancellationToken)
    {
        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT public.save_visitor_and_pending(@p_apartment_id, @p_first_name, @p_last_name, @p_email, @p_contact_number, @p_vehicle_number, @p_identity_type_id, @p_identity_number, @p_visitor_type_id, @p_visiting_from, @p_created_by)";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_apartment_id", NpgsqlDbType.Uuid) { Value = request.ApartmentId });
        command.Parameters.Add(new NpgsqlParameter("p_first_name", NpgsqlDbType.Text) { Value = request.FirstName });
        command.Parameters.Add(new NpgsqlParameter("p_last_name", NpgsqlDbType.Text) { Value = request.LastName });
        command.Parameters.Add(new NpgsqlParameter("p_email", NpgsqlDbType.Text) { Value = request.Email });
        command.Parameters.Add(new NpgsqlParameter("p_contact_number", NpgsqlDbType.Text) { Value = request.ContactNumber });
        command.Parameters.Add(new NpgsqlParameter("p_vehicle_number", NpgsqlDbType.Text) { Value = request.VehicleNumber });
        command.Parameters.Add(new NpgsqlParameter("p_identity_type_id", NpgsqlDbType.Integer) { Value = request.IdentityTypeId });
        command.Parameters.Add(new NpgsqlParameter("p_identity_number", NpgsqlDbType.Text) { Value = request.IdentityNumber });
        command.Parameters.Add(new NpgsqlParameter("p_visitor_type_id", NpgsqlDbType.Integer) { Value = request.VisitorTypeId });
        command.Parameters.Add(new NpgsqlParameter("p_visiting_from", NpgsqlDbType.Text) { Value = request.VisitingFrom });
        command.Parameters.Add(new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) { Value = request.CreatedBy });

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result is not int visitorId)
        {
            return Result.Failure<EntityCreatedResponse>(Errors.General.EntityNotFound);
        }

        await _mediator.Publish(new VisitorCreatedEvent(visitorId), cancellationToken);

        return Result.Success(new EntityCreatedResponse(visitorId));
    }


    //public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorPendingCommand request, CancellationToken cancellationToken)
    //{
    //    var parameters = new[]
    //    {
    //        new NpgsqlParameter("p_apartment_id", NpgsqlDbType.Uuid) { Value = request.ApartmentId },
    //        new NpgsqlParameter("p_first_name", NpgsqlDbType.Text) { Value = request.FirstName },
    //        new NpgsqlParameter("p_last_name", NpgsqlDbType.Text) { Value = request.LastName },
    //        new NpgsqlParameter("p_email", NpgsqlDbType.Text) { Value = request.Email },
    //        new NpgsqlParameter("p_contact_number", NpgsqlDbType.Text) { Value = request.ContactNumber },
    //        new NpgsqlParameter("p_vehicle_number", NpgsqlDbType.Text) { Value = request.VehicleNumber },
    //        new NpgsqlParameter("p_identity_type_id", NpgsqlDbType.Integer) { Value = request.IdentityTypeId },
    //        new NpgsqlParameter("p_identity_number", NpgsqlDbType.Text) { Value = request.IdentityNumber },
    //        new NpgsqlParameter("p_visitor_type_id", NpgsqlDbType.Integer) { Value = request.VisitorTypeId },
    //        new NpgsqlParameter("p_visiting_from", NpgsqlDbType.Text) { Value = request.VisitingFrom },
    //        new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) { Value = request.CreatedBy }
    //    };

    //    await _dbContext.Database.ExecuteSqlRawAsync(
    //        "select * from public.save_visitor_and_pending(@p_apartment_id, @p_first_name, @p_last_name, @p_email, @p_contact_number, @p_vehicle_number, @p_identity_type_id, @p_identity_number, @p_visitor_type_id, @p_visiting_from, @p_created_by)",
    //        parameters
    //    );

    //    await _mediator.Publish(new VisitorCreatedEvent(request.VisitorId), cancellationToken);
    //    return Result.Success(new EntityCreatedResponse(request.VisitorId));
    //}
    #endregion
}

