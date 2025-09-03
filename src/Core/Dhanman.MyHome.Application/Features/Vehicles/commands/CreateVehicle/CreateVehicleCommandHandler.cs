using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Features.Vehicles.commands.CreateVehicle;
using Dhanman.MyHome.Application.Features.Vehicles.Events;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.Vehicles.commands.create;

public class CreateVehicleCommandHandler(IApplicationDbContext _dbContext, IMediator _mediator) : ICommandHandler<CreateVehicleCommand, Result<EntityCreatedResponse>>
{
    public async Task<Result<EntityCreatedResponse>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var parameters = new[]
        {
                new NpgsqlParameter("p_vehicle_number", NpgsqlDbType.Text)
                    { Value = string.IsNullOrEmpty(request.VehicleNumber) ? DBNull.Value : (object)request.VehicleNumber },
                new NpgsqlParameter("p_vehicle_type_id", NpgsqlDbType.Integer)
                    { Value = request.VehicleTypeId },
                new NpgsqlParameter("p_unit_id", NpgsqlDbType.Integer)
                    { Value = request.UnitId },
                new NpgsqlParameter("p_vehicle_rf_id", NpgsqlDbType.Text)
                    { Value = string.IsNullOrEmpty(request.VehicleRfId) ? DBNull.Value : (object)request.VehicleRfId },
                new NpgsqlParameter("p_vehicle_rf_id_secretcode", NpgsqlDbType.Text)
                    { Value = string.IsNullOrEmpty(request.VehicleRfIdSecretcode) ? DBNull.Value : (object)request.VehicleRfIdSecretcode },
                new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid)
                    { Value = request.CreatedBy }
            };

        await _dbContext.Database.ExecuteSqlRawAsync(
            "CALL public.create_vehicle(@p_vehicle_number, @p_vehicle_type_id, @p_unit_id, @p_vehicle_rf_id, @p_vehicle_rf_id_secretcode, @p_created_by);",
            parameters
        );
        await _mediator.Publish(new VehicleCreatedEvent(request.VehicleId), cancellationToken);

        return Result.Success(new EntityCreatedResponse(request.VehicleId));
    }
}
