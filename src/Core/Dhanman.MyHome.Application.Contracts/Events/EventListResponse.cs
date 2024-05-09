using Dhanman.MyHome.Application.Contracts.Residents;

namespace Dhanman.MyHome.Application.Contracts.Events;

public sealed class EventListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<EventResponse> Items { get; }
    #endregion

    #region Constructor

    public EventListResponse(IReadOnlyCollection<EventResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
