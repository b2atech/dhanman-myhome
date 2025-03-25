using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using System;

namespace Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Commands.CreateTicketServiceProviderOtp
{
    public class CreateTicketServiceProviderOtpCommand : ICommand<Result<EntityCreatedResponse>>
    {
        public string Otp { get; set; }
        public DateTime ExpirationTime { get; set; }
        public Guid TicketId { get; set; }

        public CreateTicketServiceProviderOtpCommand(string otp, DateTime expirationTime, Guid ticketId)
        {
            Otp = otp;
            ExpirationTime = expirationTime;
            TicketId = ticketId;
        }
    }
}
