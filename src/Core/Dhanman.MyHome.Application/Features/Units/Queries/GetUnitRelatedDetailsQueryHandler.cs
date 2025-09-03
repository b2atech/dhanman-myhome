using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitRelatedDetailsQueryHandler : IQueryHandler<GetUnitRelatedDetailsQuery, Result<UnitRelatedDetailsDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetUnitRelatedDetailsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Result<UnitRelatedDetailsDto>> Handle(GetUnitRelatedDetailsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(q => q.UnitId > 0, Errors.General.EntityNotFound)
            .Bind(async q =>
            {
                await using var connection = _dbContext.Database.GetDbConnection();
                await connection.OpenAsync(cancellationToken);

                await using var command = connection.CreateCommand();
                command.CommandText = "SELECT get_unit_related_details(@UnitId) AS unit_data";
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("UnitId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = q.UnitId });

                var jsonResultObj = await command.ExecuteScalarAsync(cancellationToken);

                if (jsonResultObj == null || jsonResultObj == DBNull.Value)
                    return Result.Failure<UnitRelatedDetailsDto>(Errors.General.EntityNotFound);

                var jsonResult = jsonResultObj.ToString();

                var unitDetails = System.Text.Json.JsonSerializer.Deserialize<UnitRelatedDetailsDto>(jsonResult);

                return Result.Success(unitDetails);
            });
    }

}

