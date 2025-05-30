using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Queries;

public sealed class GetWaterTankerSummaryQueryHandler : IQueryHandler<GetWaterTankerSummaryQuery, Result<WaterTankerSummaryResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetWaterTankerSummaryQueryHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Result<WaterTankerSummaryResponse>> Handle(GetWaterTankerSummaryQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
             .Ensure(q => q != null, Errors.General.EntityNotFound)
             .Bind(async query =>
             {
                 const string sql = "SELECT * FROM public.get_water_tanker_summary( @p_company_id::uuid,@p_start_date::date,@p_end_date::date)";

                 await using var conn = _dbContext.Database.GetDbConnection();
                 await conn.OpenAsync(cancellationToken);

                 await using var cmd = conn.CreateCommand();
                 cmd.CommandText = sql;
                 cmd.Parameters.Add(new NpgsqlParameter("p_company_id", request.CompanyId));
                 cmd.Parameters.Add(new NpgsqlParameter("p_start_date", request.StartDate));
                 cmd.Parameters.Add(new NpgsqlParameter("p_end_date", request.EndDate));

                 await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

                 if (await reader.ReadAsync(cancellationToken))
                 {
                     var response = new WaterTankerSummaryResponse
                     (
                         reader.GetDateTime(reader.GetOrdinal("start_date")),
                         reader.GetDateTime(reader.GetOrdinal("end_date")),
                         reader.GetInt32(reader.GetOrdinal("total_tankers")),
                         reader.GetDecimal(reader.GetOrdinal("total_liters"))
                     );

                     return response;
                 }

                 return new WaterTankerSummaryResponse(request.StartDate, request.EndDate, 0, 0);
             });
    }
}
