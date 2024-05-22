namespace Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

public sealed class AssignSPUnitsListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<AssignSPUnitsResponse> Items { get; }
    #endregion

    #region Constructor

    public AssignSPUnitsListResponse(IReadOnlyCollection<AssignSPUnitsResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
