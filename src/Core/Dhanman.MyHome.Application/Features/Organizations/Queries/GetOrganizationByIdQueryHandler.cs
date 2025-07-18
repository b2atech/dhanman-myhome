using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Organizations;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Organizations;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Organizations.Queries;

public class GetOrganizationByIdQueryHandler : IQueryHandler<GetOrganizationByIdQuery, Result<OrganizationResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetOrganizationByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<Result<OrganizationResponse>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var company = await _dbContext.Set<Organization>()
                .AsNoTracking()
                .Where(o => o.Id == request.OrganizationId)
                .Select(e => new OrganizationResponse(
                    e.Id,
                    e.Name
                   ))
                .FirstOrDefaultAsync(cancellationToken);

                return company != null
                    ? Result.Success(company)
                    : Result.Failure<OrganizationResponse>(Errors.General.EntityNotFound);
            });
    }
    #endregion
}
