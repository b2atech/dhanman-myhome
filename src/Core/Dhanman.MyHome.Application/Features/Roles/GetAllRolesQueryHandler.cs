using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Roles;
using Dhanman.MyHome.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Roles;

public sealed class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, Result<RoleListResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllRolesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<RoleListResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM public.get_all_roles()";

        var roles = await _dbContext.SetInt<RoleDto>()
            .FromSqlRaw(sql)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var response = roles.Select(r => new RoleResponse(r.Id, r.Name, r.Description)).ToList();

        return new RoleListResponse(response);
    }
}