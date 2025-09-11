using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitsWithPrimaryOwnerQueryHandler : IQueryHandler<GetUnitsWithPrimaryOwnerQuery, Result<UnitOwnerNameListResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetUnitsWithPrimaryOwnerQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Result<UnitOwnerNameListResponse>> Handle(GetUnitsWithPrimaryOwnerQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var sql = "SELECT * FROM public.get_units_with_primary_owner_name(@p_apartment_id)";

                var units = await _dbContext.SetInt<UnitOwnerNameEntity>()
                    .FromSqlRaw(sql, new NpgsqlParameter("p_apartment_id", request.ApartmentId))
                    .AsNoTracking()
                    .Select(u => new UnitOwnerNameResponse(
                        u.Id,
                        u.CustomerId,
                        u.UnitId,
                        u.UnitName,
                        u.OwnerFirstName,
                        u.OwnerLastName
                    ))
                    .ToListAsync(cancellationToken);

                return new UnitOwnerNameListResponse(units);
            });
    }
}