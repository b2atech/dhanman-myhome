using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Organizations;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Organizations.Queries;

public class GetOrganizationByIdQuery : ICacheableQuery<Result<OrganizationResponse>>
{
    #region Properties
    public Guid OrganizationId { get; }
    #endregion

    #region Constructors
    public GetOrganizationByIdQuery(Guid organizationId)
    {
        OrganizationId = organizationId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Organizations.Organization, "OrganizationId");
    #endregion

}
