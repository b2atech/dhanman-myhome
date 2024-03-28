namespace Dhanman.MyHome.Application.Contracts.Residents;

public sealed class ResidentListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<ResidentResponse> Items { get; }
    #endregion

    #region Constructor

    public ResidentListResponse(IReadOnlyCollection<ResidentResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}