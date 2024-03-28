namespace Dhanman.MyHome.Application.Contracts.Residents;

public sealed class ResidentNameListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<ResidentNameResponse> Items { get; }
    #endregion

    #region Constructor

    public ResidentNameListResponse(IReadOnlyCollection<ResidentNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}