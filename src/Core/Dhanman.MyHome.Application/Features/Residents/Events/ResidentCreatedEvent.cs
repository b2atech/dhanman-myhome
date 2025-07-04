using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Residents.Events;
public class ResidentCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }
    #endregion

    #region Constructors
    public ResidentCreatedEvent(int id) => Id = id;
    #endregion

}