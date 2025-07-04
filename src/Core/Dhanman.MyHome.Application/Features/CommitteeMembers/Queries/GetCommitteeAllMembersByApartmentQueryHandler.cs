using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.CommitteeMembers;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.CommitteeMembers.Queries;

public sealed class GetCommitteeAllMembersByApartmentQueryHandler
    : IQueryHandler<GetCommitteeAllMembersByApartmentQuery, Result<CommitteeAllMemberListResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetCommitteeAllMembersByApartmentQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CommitteeAllMemberListResponse>> Handle(GetCommitteeAllMembersByApartmentQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query.ApartmentId != Guid.Empty, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                const string sql = "SELECT * FROM public.get_all_committee_members_by_apartment(@p_apartment_id)";
                var parameters = new[]
                {
                    new NpgsqlParameter("p_apartment_id", query.ApartmentId)
                };

                var rawResult = await _dbContext.SetInt<CommitteeAllMemberDto>()
                    .FromSqlRaw(sql, parameters)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var response = rawResult.Select(e => new CommitteeAllMemberResponse(
                    e.Id,
                    e.UserId,
                    e.MemberName,
                    e.EffectiveStartDate,
                    e.EffectiveEndDate,
                    e.RoleId,
                    e.RoleName,
                    e.PortfolioId,
                    e.PortfolioName
                )).ToList();

                return new CommitteeAllMemberListResponse(response);
            });
    }
}