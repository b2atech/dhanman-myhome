using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Units.Event
{
    public sealed class UnitUpdateEvent : IEvent
    {
        #region Properties
        public int UnitId { get; set; }

        #endregion

        #region Constructor
        public UnitUpdateEvent(int id) => UnitId = id;

        #endregion
    }
}
