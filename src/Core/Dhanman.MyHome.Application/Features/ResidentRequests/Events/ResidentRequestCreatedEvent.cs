using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Events;

public class ResidentRequestCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }
    #endregion

    #region Constructors
    public ResidentRequestCreatedEvent(int id) => Id = id;
    #endregion

}