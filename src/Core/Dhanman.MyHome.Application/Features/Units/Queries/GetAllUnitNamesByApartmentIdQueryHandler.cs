using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Units;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitNamesByApartmentIdQueryHandler : IRequestHandler<GetAllUnitNamesByApartmentIdQuery, Result<UnitNamesWithApartmentListResponse>>
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetAllUnitNamesByApartmentIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<UnitNamesWithApartmentListResponse>> Handle(GetAllUnitNamesByApartmentIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var sql = "SELECT * FROM public.get_all_unit_names_by_apartment_id(@p_apartment_id)";

                var parameters = new[] {
                new NpgsqlParameter("p_apartment_id", request.ApartmentId)
                };

                var unitEntities = await _dbContext.SetInt<UnitNamesByAparmentEntity>()
                    .FromSqlRaw(sql, parameters)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var units = unitEntities.Select(u => new UnitNamesWithApartmentResponse(
                    u.Id,
                    u.UnitId,
                    u.UnitName,
                    u.BuildingName,
                    u.FloorName
                )).ToList();

                return new UnitNamesWithApartmentListResponse(units);
            });

        return result;
    }

    #endregion
}