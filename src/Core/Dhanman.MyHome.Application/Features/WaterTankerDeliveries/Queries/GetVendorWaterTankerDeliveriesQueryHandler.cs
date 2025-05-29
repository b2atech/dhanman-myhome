using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
                const string sql = "SELECT * FROM public.get_water_tanker_deliveries(@p_company_id, @p_start_date::date, @p_end_date::date, @p_vendor_id)";

                await using var conn = _dbContext.Database.GetDbConnection();
                await conn.OpenAsync(cancellationToken);

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.Add(new NpgsqlParameter("p_company_id", request.CompanyId));
                cmd.Parameters.Add(new NpgsqlParameter("p_start_date", request.StartDate));
                cmd.Parameters.Add(new NpgsqlParameter("p_end_date", request.EndDate));
                cmd.Parameters.Add(new NpgsqlParameter("p_vendor_id", request.VendorId));

                var items = new List<WaterTankerDeliveryResponse>();

                await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    items.Add(new WaterTankerDeliveryResponse
                    {
                        Id = reader.GetInt32(0),
                        DeliveryDate = reader.GetDateTime(1),
                        DeliveryTime = await reader.GetFieldValueAsync<TimeSpan>(2, cancellationToken),
                        VendorId = reader.GetGuid(3),
                        VendorName = reader.IsDBNull(4) ? null : reader.GetString(4),
                        TankerCapacityLiters = reader.GetInt32(5),
                        ActualReceivedLiters = reader.GetInt32(6),
                        CreatedBy = reader.GetGuid(7),
                        CreatedByName = reader.IsDBNull(8) ? null : reader.GetString(8),
                        CreatedOnUtc = reader.GetDateTime(9),
                        ModifiedBy = reader.IsDBNull(10) ? (Guid?)null : reader.GetGuid(10),
                        ModifiedByName = reader.IsDBNull(11) ? null : reader.GetString(11),
                        ModifiedOnUtc = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12)
                    });
                }

                return new WaterTankerDeliveryListResponse(items);
            });
    }
}