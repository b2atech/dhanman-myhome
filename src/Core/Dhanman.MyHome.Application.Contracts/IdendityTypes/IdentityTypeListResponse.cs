namespace Dhanman.MyHome.Application.Contracts.IdendityTypes;

public sealed class IdentityTypeListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<IdendityTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public IdentityTypeListResponse(IReadOnlyCollection<IdendityTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
