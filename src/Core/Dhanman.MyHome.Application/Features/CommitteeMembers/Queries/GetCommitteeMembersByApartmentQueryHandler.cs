using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.CommitteeMembers;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.CommitteeMembers;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Application.Features.CommitteeMembers.Queries;

public class GetCommitteeMembersByApartmentQueryHandler : IQueryHandler<GetCommitteeMembersByApartmentQuery, Result<List<CommitteeMemberResponse>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetCommitteeMembersByApartmentQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<CommitteeMemberResponse>>> Handle(GetCommitteeMembersByApartmentQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(q => q != null, Errors.General.EntityNotFound)
            .Bind(async q =>
            {
                const string sqlFunction = "SELECT * FROM public.get_committee_members_by_apartment(@p_apartment_id)";
                var parameters = new[]
                {
                    new NpgsqlParameter("p_apartment_id", request.ApartmentId)
                };

                var data = await _dbContext.SetInt<CommitteeMemberDto>()
                    .FromSqlRaw(sqlFunction, parameters)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var response = data.Select(item => new CommitteeMemberResponse(
                    item.Id,
                    item.ApartmentId,
                    item.RoleId,
                    item.UserId,
                    item.MemberName
                )).ToList();

                return Result.Success(response);
            });
    }
}
