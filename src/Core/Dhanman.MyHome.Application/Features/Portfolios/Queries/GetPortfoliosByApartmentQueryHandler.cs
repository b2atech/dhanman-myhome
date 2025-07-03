using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Portfolios;
using Dhanman.MyHome.Domain.Entities.Portfolios;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.Portfolios.Queries;

public sealed class GetPortfoliosByApartmentQueryHandler : IQueryHandler<GetPortfoliosByApartmentQuery, Result<PortfolioListResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetPortfoliosByApartmentQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PortfolioListResponse>> Handle(GetPortfoliosByApartmentQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Bind(async query =>
            {
                const string sql = "SELECT * FROM public.get_portfolios_by_apartment(@p_apartment_id)";

                var parameters = new[]
                {
                    new NpgsqlParameter("p_apartment_id", query.ApartmentId)
                };

                var raw = await _dbContext.SetInt<PortfolioDto>()
                    .FromSqlRaw(sql, parameters)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var response = raw.Select(e => new PortfolioResponse(
                    e.Id,
                    e.ApartmentId,
                    e.Name,
                    e.Description)).ToList();

                return new PortfolioListResponse(response);
            });
    }
}