namespace Dhanman.MyHome.Application.Contracts.ResidentRequests;

public sealed class ResidentRequestListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<ResidentRequestResponse> Items { get; }
    #endregion

    #region Constructor

    public ResidentRequestListResponse(IReadOnlyCollection<ResidentRequestResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}