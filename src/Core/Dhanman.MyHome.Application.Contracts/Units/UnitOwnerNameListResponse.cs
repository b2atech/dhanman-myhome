namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitOwnerNameListResponse
{
    public IReadOnlyCollection<UnitOwnerNameResponse> Items { get; }
    public string Cursor { get; }

    public UnitOwnerNameListResponse(IReadOnlyCollection<UnitOwnerNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
}
