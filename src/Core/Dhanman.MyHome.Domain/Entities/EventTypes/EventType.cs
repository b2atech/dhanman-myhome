using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.EventTypes;

public  class EventType : EntityInt
{
    #region Properties
    public string Name { get; set; }
    #endregion

    #region Constructor
    public EventType()
    {

    }
    public EventType(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
