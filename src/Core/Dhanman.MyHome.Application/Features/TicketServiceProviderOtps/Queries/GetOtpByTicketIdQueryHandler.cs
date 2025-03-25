using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.TicketServiceProviderOtps;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Queries
{
    public class GetOtpByTicketIdQueryHandler : IQueryHandler<GetOtpByTicketIdQuery, Result<TicketServiceProviderOtpResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetOtpByTicketIdQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Methods
        public async Task<Result<TicketServiceProviderOtpResponse>> Handle(GetOtpByTicketIdQuery request, CancellationToken cancellationToken)
        {
            return await Result.Success(request)
                .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
                {
                    var otpList = await (from otp in _dbContext.Set<TicketServiceProviderOtp>().AsNoTracking()
                                         where otp.TicketId == request.TicketId
                                         select new TicketServiceProviderOtpResponse
                                         {
                                             Id = otp.Id,
                                             Otp = otp.Otp,
                                             ExpirationTime = otp.ExpirationTime,
                                             TicketId = otp.TicketId,
                                         })
                                         .FirstOrDefaultAsync(cancellationToken);
                    return otpList !=null
                        ? Result.Success(otpList)
                        : Result.Failure<TicketServiceProviderOtpResponse>(Errors.General.EntityNotFound);
                });
        }
        #endregion
    }
}
