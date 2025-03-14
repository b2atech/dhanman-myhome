﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Tickets.Commands.UpdateTicket;

public sealed class UpdateTicketServiceProviderCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties

    public Guid TicketId { get; set; }
    public int ServiceProviderId { get; set; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructor
    public UpdateTicketServiceProviderCommand(Guid ticketId, int serviceProviderId,Guid createdBy)
    {
        TicketId = ticketId;
        ServiceProviderId = serviceProviderId;
        CreatedBy = createdBy;
    }
    #endregion
}
