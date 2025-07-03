using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Tickets;
using System;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetAllServiceProviderTicketCategoryQuery : ICacheableQuery<Result<ServiceProviderTicketCategoryListResponse>>
{

    #region Constructors
    public GetAllServiceProviderTicketCategoryQuery()
    {
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Tickets.ServiceProviderCategoryList);
    #endregion
}
