using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Domain.Entities.Tickets;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;
using Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Event;

namespace Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Commands.CreateTicketServiceProviderOtp
{
    public class CreateTicketServiceProviderOtpCommandHandler : ICommandHandler<CreateTicketServiceProviderOtpCommand, Result<EntityCreatedResponse>>
    {
        private readonly ITicketServiceProviderOtpRepository _ticketServiceProviderOtpRepository;
        private readonly IMediator _mediator;

        public CreateTicketServiceProviderOtpCommandHandler(ITicketServiceProviderOtpRepository repository, IMediator mediator)
        {
            _ticketServiceProviderOtpRepository = repository;
            _mediator = mediator;
        }

        public async Task<Result<EntityCreatedResponse>> Handle(CreateTicketServiceProviderOtpCommand request, CancellationToken cancellationToken)
        {
            var otp = new TicketServiceProviderOtp
            {
                Otp = request.Otp,
                ExpirationTime = request.ExpirationTime,
                TicketId = request.TicketId,
            };
            _ticketServiceProviderOtpRepository.Insert(otp);

            await _mediator.Publish(new TicketServiceProviderOtpCreatedEvent(otp.Id), cancellationToken);
            return Result.Success(new EntityCreatedResponse(otp.Id));

        }
    }
}
