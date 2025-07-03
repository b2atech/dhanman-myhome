using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands;

public sealed class CreateWaterTankerDeliveriesBulkCommandHandler : ICommandHandler<CreateWaterTankerDeliveriesBulkCommand, Result<EntityCreatedResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IUserContextService _userContext;

    public CreateWaterTankerDeliveriesBulkCommandHandler(IApplicationDbContext dbContext, IUserContextService userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    public async Task<Result<EntityCreatedResponse>> Handle(CreateWaterTankerDeliveriesBulkCommand request, CancellationToken cancellationToken)
    {

        var trimmedDeliveries = request.Deliveries
            .Select(d => new
            {
                d.CompanyId,
                d.VendorId,
                d.DeliveryDate,
                d.DeliveryTime,
                d.TankerCapacityLiters,
                d.ActualReceivedLiters
            }).ToList();


        var json = JsonSerializer.Serialize(trimmedDeliveries, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        // Add this line for debugging:
        System.Diagnostics.Debug.WriteLine(json);

        await using var connection = _dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT insert_bulk_water_tanker_deliveries(@p_deliveries::jsonb, @p_created_by, @p_created_on_utc);";
        command.CommandType = System.Data.CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_deliveries", NpgsqlDbType.Jsonb) {Value = json});
        command.Parameters.Add(new NpgsqlParameter("p_created_by", NpgsqlDbType.Uuid) {Value = _userContext.CurrentUserId});
        command.Parameters.Add(new NpgsqlParameter("p_created_on_utc", NpgsqlDbType.Date) {Value = DateTime.UtcNow});

        var result = (int)await command.ExecuteScalarAsync(cancellationToken);

        return Result.Success(new EntityCreatedResponse(result));
    }
}
