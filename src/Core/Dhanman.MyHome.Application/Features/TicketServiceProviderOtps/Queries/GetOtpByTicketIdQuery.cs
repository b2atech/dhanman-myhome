using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.TicketServiceProviderOtps;

namespace Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Queries
{
    public class GetOtpByTicketIdQuery : ICacheableQuery<Result<TicketServiceProviderOtpResponse>>
    {
        #region Properties
        public Guid TicketId { get; set; }
        #endregion

        #region Constructor
        public GetOtpByTicketIdQuery(Guid ticketId)
        {
            TicketId = ticketId;
        }
        #endregion

        #region Methods
        public string GetCacheKey() => string.Format(CacheKeys.TicketServiceProviderOtps.OtpByTicketId, TicketId);
        #endregion
    }
}
