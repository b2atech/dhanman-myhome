using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Vehicles.commands.create;
using Dhanman.MyHome.Application.Features.Vehicles.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.Vehicles.Commands.Create
{
    public class CreateVisitorVehicleCommandHandler : ICommandHandler<CreateVisitorVehicleCommand, Result<EntityCreatedResponse>>
    {
        #region Properties
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public CreateVisitorVehicleCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        #endregion

        #region Methods
        public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorVehicleCommand request, CancellationToken cancellationToken)
        {
            var parameters = new[]
            {
                new NpgsqlParameter("p_visitor_log_id", NpgsqlDbType.Integer) { Value = request.VisitorLogId },
                new NpgsqlParameter("p_vehicle_number", NpgsqlDbType.Text) { Value = string.IsNullOrEmpty(request.VehicleNumber) ? DBNull.Value : (object)request.VehicleNumber },
                new NpgsqlParameter("P_vehicle_type", NpgsqlDbType.Text) { Value = string.IsNullOrEmpty(request.VehicleType) ? DBNull.Value : (object)request.VehicleType }
            };

            await _dbContext.Database.ExecuteSqlRawAsync(
                "CALL public.insert_visitor_vehicle(@p_visitor_log_id, @p_vehicle_number, @P_vehicle_type);",
                parameters
            );

            await _mediator.Publish(new VisitorVehicleCreatedEvent(request.VisitorVehicleId), cancellationToken);
            return Result.Success(new EntityCreatedResponse(request.VisitorVehicleId));
        }
        #endregion
    }
}
