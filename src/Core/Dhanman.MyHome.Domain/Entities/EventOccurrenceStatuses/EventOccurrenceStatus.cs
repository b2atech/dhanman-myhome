using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.EventOccurrenceStatuses;

public class EventOccurrenceStatus : EntityInt

{
    #region Properties
    public string Name { get; private set; }
    #endregion

    #region Constructors
    public EventOccurrenceStatus() { }
    public EventOccurrenceStatus(string name, Guid createdBy)
    {
        Name = name;
    }
    #endregion

}

