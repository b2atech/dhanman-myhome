using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Queries;

public sealed class GetVendorWaterTankerDeliveriesQueryHandler : IQueryHandler<GetVendorWaterTankerDeliveriesQuery, Result<WaterTankerDeliveryListResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetVendorWaterTankerDeliveriesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Result<WaterTankerDeliveryListResponse>> Handle(GetVendorWaterTankerDeliveriesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(q => q != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var items = await ExecuteWaterTankerDeliveriesQuery(query, cancellationToken);
                return new WaterTankerDeliveryListResponse(items);
            });
    }

    private async Task<List<WaterTankerDeliveryResponse>> ExecuteWaterTankerDeliveriesQuery(GetVendorWaterTankerDeliveriesQuery query, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM public.get_water_tanker_deliveries(@p_company_id, @p_start_date::date, @p_end_date::date, @p_vendor_id)";
        await using var conn = _dbContext.Database.GetDbConnection();
        await conn.OpenAsync(cancellationToken);
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.Add(new NpgsqlParameter("p_company_id", query.CompanyId));
        cmd.Parameters.Add(new NpgsqlParameter("p_start_date", query.StartDate));
        cmd.Parameters.Add(new NpgsqlParameter("p_end_date", query.EndDate));
        cmd.Parameters.Add(new NpgsqlParameter("p_vendor_id", query.VendorId));

        var items = new List<WaterTankerDeliveryResponse>();
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(await ReadWaterTankerDeliveryAsync(reader, cancellationToken));
        }
        return items;
    }

    private async Task<WaterTankerDeliveryResponse> ReadWaterTankerDeliveryAsync(DbDataReader reader, CancellationToken cancellationToken)
    {
        return new WaterTankerDeliveryResponse
        {
            Id = reader.GetInt32(0),
            DeliveryDate = reader.GetDateTime(1),
            DeliveryTime = await reader.GetFieldValueAsync<TimeSpan>(2, cancellationToken),
            VendorId = reader.GetGuid(3),
            VendorName = await reader.IsDBNullAsync(4, cancellationToken) ? null : reader.GetString(4),
            TankerCapacityLiters = reader.GetInt32(5),
            ActualReceivedLiters = reader.GetInt32(6),
            CreatedBy = reader.GetGuid(7),
            CreatedByName = await reader.IsDBNullAsync(8, cancellationToken) ? null : reader.GetString(8),
            CreatedOnUtc = reader.GetDateTime(9),
            ModifiedBy = await reader.IsDBNullAsync(10, cancellationToken) ? (Guid?)null : reader.GetGuid(10),
            ModifiedByName = await reader.IsDBNullAsync(11, cancellationToken) ? null : reader.GetString(11),
            ModifiedOnUtc = await reader.IsDBNullAsync(12, cancellationToken) ? (DateTime?)null : reader.GetDateTime(12)
        };
    }


}